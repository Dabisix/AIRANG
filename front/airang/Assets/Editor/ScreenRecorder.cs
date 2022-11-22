using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Recorder.Input;
using UnityEditor.Recorder;
using UnityEditor;
using UnityEngine;

public class ScreenRecorder : MonoBehaviour
{
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
}
