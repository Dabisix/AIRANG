using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingResume : MonoBehaviour
{
    void Start()
    {
        var wcc = WebCamController.getInstance();
        if (wcc.isRecording)
        {
            Debug.Log("wcc : " + wcc.prevIsARScene);
            if (wcc.prevIsARScene)
            {
                // if previous scene is AR, It shoud re start recording
                Debug.Log("wcc : prev is AR, recording start again");
                wcc.prevIsARScene = false;
                wcc.getFrontCamera();
                wcc.startRecording();
            } else
            {
                // just resume recording
                wcc.resumeRecording();
            }
        }

        wcc.prevIsARScene = false;
    }
}
