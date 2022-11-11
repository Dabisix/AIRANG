from fastapi import APIRouter
from pydantic import BaseModel
from routers import textPreprocessing

import os
import numpy as np
from scipy.io.wavfile import write
import IPython
from TTS.utils.synthesizer import Synthesizer
from pydub import AudioSegment
import pysftp


class RequestTTS(BaseModel):
    email: str
    title: str
    text: list


router = APIRouter(
    prefix="/api/ai",
    responses={404: {"description": "Not Found"}},
)


@router.post("/", tags=["tts"])
async def get_ai_voice(request: RequestTTS):
    # PRIVATE VALUES ###############################################################################################
    host_name = "ssh-ed25519"
    user_name = "ubuntu"
    host_url = "k7b305.p.ssafy.io"
    pem_path = "K7B305T.pem"
    ffmpeg_path = "C:/ffmpeg/bin/ffmpeg.exe"
    glowtts_path = "/content/drive/My Drive/Colab Notebooks/data/glowtts-v2/glowtts-v2-November-03-2022_08+45PM-3aa165a/"
    hifigan_path = "/content/drive/My Drive/Colab Notebooks/data/hifigan-v2/"

    # host_name = "HOST NAME"
    # user_name = "USER NAME"
    # host_url = "HOST URL"
    # pem_path = "PEM KEY"
    # ffmpeg_path = 'FFMPEG PATH'
    # glowtts_path = "GLOWTTSPATH"
    # hifigan_path = "HIGIGAN PATH"
    ###############################################################################################################

    try:
        synthesizer = Synthesizer(
            glowtts_path + "best_model.pth.tar",
            glowtts_path + "/config.json",
            None,
            hifigan_path + "model_file.pth.tar",
            hifigan_path + "config.json",
            # "/content/drive/My Drive/Colab Notebooks/data/hifigan-v2/hifigan-v2-November-07-2022_02+15AM-3aa165a/best_model.pth.tar",
            # "/content/drive/My Drive/Colab Notebooks/data/hifigan-v2/hifigan-v2-November-07-2022_02+15AM-3aa165a/config.json",
            None,
            None,
            False,
        )
        symbols = synthesizer.tts_config.characters.characters

        rate = 22050

        # host key checking
        # cnopts = pysftp.CnOpts(knownhosts=host_name)

        # temporary disable host key checking..
        cnopts = pysftp.CnOpts()
        cnopts.hostkeys = None

        remote_path = "/home/" + user_name + "/tts/voice/ai/" + request.email + "/" + request.title + "/"
        idx = 0

        for text in textPreprocessing.normalize_multiline_text(request.text, symbols):
            wav = synthesizer.tts(text, None, None)
            IPython.display.display(IPython.display.Audio(wav, rate=22050))

            scaled = np.int16(wav / np.max(np.abs(wav)) * 32767)
            file_name = str(idx)
            write(file_name + '.wav', rate, scaled)

            AudioSegment.converter = ffmpeg_path
            sound = AudioSegment.from_wav(file_name + '.wav')
            sound.export(file_name + ".mp3", format="mp3")
            os.remove(file_name + ".wav")

            try:
                with pysftp.Connection(host_url, username=user_name, private_key=pem_path, cnopts=cnopts) as sftp:
                    print("local_path => ((", file_name, ".mp3)) ===> remote_path ((", remote_path, file_name, ".mp3))")
                    sftp.put(file_name + ".mp3", remote_path + file_name + ".mp3")
                    os.remove(file_name + ".mp3")
                    sftp.close()
            except:
                return "FAIL TO SFTP"

            idx += 1

    except:
        return "FAIL TO VOICE CONVERSION"

    return "SUCCESS"


