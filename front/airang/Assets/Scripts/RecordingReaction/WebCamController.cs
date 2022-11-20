using NatML.Recorders.Clocks;
using NatML.Recorders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using NatML.Recorders.Inputs;
using UnityEngine.SceneManagement;
using System.IO;

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
    public bool isRecording = false;
    private string bookname;
    private string date;


    private void Start()
    {
        // initilize camera
        getFrontCamera();
        // setMicrophone();
    }

    public void startRecording()
    {
        if (isRecording) return;

        isRecording = true;

        bookname = BookManager.getInstance().BookName;
        date = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

        recordingClock = new RealtimeClock();
        startScreenRecording();
        startCamRecording();
    }

    public void pauseRecording()
    {
        if (!isRecording) return;

        turnOffScreen();
        turnOffWebCamTexture();
        // webCamTexture.Pause();
    }

    public void resumeRecording()
    {
        if (!isRecording) return;

        turnOnScreen();
        turnOnWebCamTexture();
    }

    public void stopRecording()
    {
        if (!isRecording) return;

        isRecording = false;

        stopScreenRecording();
        stopCamRecording();
    }

    #region WebCam
    private WebCamTexture webCamTexture;
    private MP4Recorder camRecorder;

    private AudioInput audioInput;
    private AudioSource microphoneSource;

    private Coroutine recordVideoCoroutine;

    public WebCamTexture WebCam
    {
        get => webCamTexture;
    }

    public void getFrontCamera()
    {
        // setting webcamra front
        string frontCamName = null;
        var webCamDevices = WebCamTexture.devices;
        foreach (var camDevice in webCamDevices)
        {
            Debug.Log("Cam " + camDevice.name);
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

    private void startCamRecording()
    {
        if (!enabled)
        {
            Debug.LogError("Component must be enabled");
            return;
        }

        turnOnWebCamTexture();

        camRecorder = new MP4Recorder(webCamTexture.width, webCamTexture.height, 30);
        // camRecorder = new MP4Recorder(webCamTexture.width, webCamTexture.height, 30, AudioSettings.outputSampleRate, (int) AudioSettings.speakerMode);

        // unmute microphone
        // audioInput = new AudioInput(camRecorder, recordingClock, microphoneSource, true);
        // microphoneSource.mute = audioInput == null;

        recordVideoCoroutine = StartCoroutine(recording());
    }

    private IEnumerator recording()
    {
        // Create a clock for generating recording timestamps
        for (int i = 0;;i++)
        {
            if(webCamTexture.isPlaying)
                camRecorder.CommitFrame(webCamTexture.GetPixels32(), recordingClock.timestamp);
            yield return new WaitForEndOfFrame();
        }
    }

    private async void stopCamRecording()
    {
        StopCoroutine(recordVideoCoroutine);

        var recordoingPath = await camRecorder.FinishWriting();

        // save to Gallery
        NativeGallery.Permission permission =
            NativeGallery.SaveVideoToGallery(recordoingPath, "airang",
                "airang_"  + date + "_" + bookname + "_cam.mp4",
                (success, path) => Debug.Log("Cam Media saved : " + success + " " + path));

        turnOffWebCamTexture();
    }

    private void turnOnWebCamTexture()
    {
        webCamTexture.Play();
    }

    private void turnOffWebCamTexture()
    {
        webCamTexture.Stop();
    }
    #endregion

    #region SCREEN
    private MP4Recorder screenRecorder;
    private ScreenInput screenInput;

    private void startScreenRecording()
    {
        screenRecorder = new MP4Recorder(Screen.width, Screen.height, 30);

        turnOnScreen();
    }

    private async void stopScreenRecording()
    {
        turnOffScreen();

        var recordoingPath = await screenRecorder.FinishWriting();

        // save to Gallery
        NativeGallery.Permission permission =
            NativeGallery.SaveVideoToGallery(recordoingPath, "airang",
                "airang_" + date + "_" + bookname + "_scr.mp4",
                (success, path) => Debug.Log("Screen Media saved : " + success + " " + path));
    }

    private void turnOnScreen()
    {
        screenInput = new ScreenInput(screenRecorder, recordingClock);
    }

    private void turnOffScreen()
    {
        screenInput.Dispose();
    }
    #endregion


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
