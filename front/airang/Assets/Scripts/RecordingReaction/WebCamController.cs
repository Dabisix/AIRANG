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
#if UNITY_EDITOR
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;

using UnityEditor;
#endif

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
        isRecording = true;
        bookname = BookManager.getInstance().BookName;
        date = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

        startCamRecording();
        startScreenRecording();
    }

    public void stopRecording()
    {
        isRecording = false;

        stopScreenRecording();
        stopCamRecording();
    }

    #region WebCam
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

    private void startCamRecording()
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

    private async void stopCamRecording()
    {
        StopCoroutine(recordVideoCoroutine);

        var recordoingPath = await recorder.FinishWriting();

        // save to Gallery
        NativeGallery.Permission permission =
            NativeGallery.SaveVideoToGallery(recordoingPath, "airang",
                bookname + "  " + date + ".mp4",
                (success, path) => Debug.Log("Media saved : " + success + " " + path));

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


    #region Screen
    RecorderController m_RecorderController;
    MovieRecorderSettings m_Settings = null;
    private bool m_RecordAudio = true;

    private FileInfo OutputFile
    {
        get
        {
            var fileName = m_Settings.OutputFile + ".mp4";
            return new FileInfo(fileName);
        }
    }

    private void startScreenRecording()
    {
        var controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        m_RecorderController = new RecorderController(controllerSettings);

        var mediaOutputFolder = new DirectoryInfo(Path.Combine(Application.dataPath, "..", "SampleRecordings"));

        m_Settings = ScriptableObject.CreateInstance<MovieRecorderSettings>();
        m_Settings.name = "Recorder";
        m_Settings.Enabled = true;


        m_Settings.OutputFormat = MovieRecorderSettings.VideoRecorderOutputFormat.MP4;
        m_Settings.VideoBitRateMode = VideoBitrateMode.High;

        m_Settings.ImageInputSettings = new GameViewInputSettings
        {
            OutputWidth = 2000,
            OutputHeight = 1200
        };

        m_Settings.AudioInputSettings.PreserveAudio = m_RecordAudio;

        // Simple file name (no wildcards) so that FileInfo constructor works in OutputFile getter.
        m_Settings.OutputFile = mediaOutputFolder.FullName + "/" + "video";

        // Setup Recording
        controllerSettings.AddRecorderSettings(m_Settings);
        controllerSettings.SetRecordModeToManual();
        controllerSettings.FrameRate = 60.0f;


        RecorderOptions.VerboseMode = false;
        m_RecorderController.PrepareRecording();
        m_RecorderController.StartRecording();

        Debug.Log($"Started recording for file {OutputFile.FullName}");
    }

    private void stopScreenRecording()
    {
        Debug.Log("End Recording");
        m_RecorderController.StopRecording();
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
