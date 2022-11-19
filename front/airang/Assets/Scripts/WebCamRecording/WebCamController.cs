using NatML.Recorders.Clocks;
using NatML.Recorders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using NatML.Recorders.Inputs;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class WebCamController : MonoBehaviour
{
    #region SINGLETON
    static WebCamController instance = null;

    // singleton Pattern implemented
    public static WebCamController getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // if object is corupted
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }
    #endregion

    private RealtimeClock recordingClock;
    private WebCamTexture webCamTexture;
    private MP4Recorder recorder;

    private AudioInput audioInput;
    private AudioSource microphoneSource;

    private Coroutine recordVideoCoroutine;

    public WebCamTexture WebCam
    {
        get => webCamTexture;
    }

    private void Start()
    {
        // initilize camera
        getFrontCamera();
        // setMicrophone();
    }

    public void testChange()
    {
        
        SceneManager.LoadScene("test2");
    }

    private void getFrontCamera()
    {
        // setting webcamra front
        string frontCamName = null;
        var webCamDevices = WebCamTexture.devices;
        foreach (var camDevice in webCamDevices)
        {
            if (camDevice.isFrontFacing)
            {
                frontCamName = camDevice.name;
                break;
            }
        }

        webCamTexture = new WebCamTexture(frontCamName);        
    }

    private IEnumerator setMicrophone()
    {
        // Start microphone
        microphoneSource = GetComponent<AudioSource>();
        microphoneSource.mute = microphoneSource.loop = true;
        microphoneSource.bypassEffects = microphoneSource.bypassListenerEffects = false;
        microphoneSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
        yield return new WaitUntil(() => Microphone.GetPosition(null) > 0);
        microphoneSource.Play();
    }

    public void startRecording()
    {
        if (!enabled)
        {
            Debug.LogError("Component must be enabled");
            return;
        }

        turnOnWebCamTexture();

        recordingClock = new RealtimeClock();
        recorder = new MP4Recorder(webCamTexture.width, webCamTexture.height, 30);
        // recorder = new MP4Recorder(webCamTexture.width, webCamTexture.height, 30, AudioSettings.outputSampleRate, (int) AudioSettings.speakerMode);

        // unmute microphone
        // audioInput = new AudioInput(recorder, recordingClock, microphoneSource, true);
        // microphoneSource.mute = audioInput == null;

        recordVideoCoroutine = StartCoroutine(recording());
    }

    private IEnumerator recording()
    {
        // Create a clock for generating recording timestamps
        var clock = new RealtimeClock();
        for (int i = 0; ; i++)
        {
            recorder.CommitFrame(webCamTexture.GetPixels32(), clock.timestamp);
            yield return new WaitForEndOfFrame();
        }
    }

    public async void stopRecording()
    {
        StopCoroutine(recordVideoCoroutine);

        var recordoingPath = await recorder.FinishWriting();

        // string bookname = BookManager.getInstance()
        string date = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

        // save to Gallery
        NativeGallery.Permission permission =
            NativeGallery.SaveVideoToGallery(recordoingPath, "airang",
                "book test " + date + ".mp4",
                (success, path) => Debug.Log("Media saved : " + success + " " + path));

        turnOffWebCamTexture();
    }

    public void turnOnWebCamTexture()
    {
        webCamTexture.Play();
    }

    public void turnOffWebCamTexture()
    {
        webCamTexture.Stop();
    }

    private void OnDestroy()
    {
        // Stop microphone
        if (microphoneSource != null)
        {
            microphoneSource.Stop();
            Microphone.End(null);
        }
    }
}
