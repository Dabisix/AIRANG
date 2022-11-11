using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System.IO;
using UnityEditor;
/*using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;*/

public class RecordingGame : MonoBehaviour
{

    /*RecorderController m_RecorderController;
    public bool m_RecordAudio = true;
    MovieRecorderSettings m_Settings = null;

    public FileInfo OutputFile {
        get {
            var fileName = m_Settings.OutputFile + ".mp4";
            return new FileInfo(fileName);
        }
    }

    public void OnClickStart()
    {
        test();
    }

    //해당 게임 화면이 시작되자마자 바로 할 거면 & 게임 화면 꺼질 때 끌 거면
    //OnEnable & OnDisable()

    void test()
    {
        var controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>(); //recorder controller 설정
        m_RecorderController = new RecorderController(controllerSettings); //recorder controller 초기화

        var mediaOutputFolder = new DirectoryInfo(Path.Combine(Application.dataPath, "..", "SampleRecordings")); //저장할 폴더인가?

        // Video 관련 셋팅
        m_Settings = ScriptableObject.CreateInstance<MovieRecorderSettings>();
        m_Settings.name = "My Video Recorder";
        m_Settings.Enabled = true;

        // This example performs an MP4 recording
        m_Settings.OutputFormat = MovieRecorderSettings.VideoRecorderOutputFormat.MP4; //MP4로 저장한다 그거일듯
        m_Settings.VideoBitRateMode = VideoBitrateMode.High; //비트레이트 높음으로 저장한다

        //이건 해상도
        m_Settings.ImageInputSettings = new GameViewInputSettings
        {
            OutputWidth = 1920,
            OutputHeight = 1080
        };

        //소리도 녹음한다
        m_Settings.AudioInputSettings.PreserveAudio = m_RecordAudio;

        //위에서 적었던 폴더 아래에 저장한다는 거일듯?
        // Simple file name (no wildcards) so that FileInfo constructor works in OutputFile getter.
        m_Settings.OutputFile = mediaOutputFolder.FullName + "/" + "video";

        //아까 세팅한거 controller에 넣어놓음
        // Setup Recording
        controllerSettings.AddRecorderSettings(m_Settings);
        controllerSettings.SetRecordModeToManual();
        controllerSettings.FrameRate = 60.0f;

        //녹화 시작!
        RecorderOptions.VerboseMode = false;
        m_RecorderController.PrepareRecording();
        m_RecorderController.StartRecording();

        Debug.Log($"Started recording for file {OutputFile.FullName}");
    }

    //녹화 멈출 때
    public void OnClickEnd()
    {
        m_RecorderController.StopRecording();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }*/
}