using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingResume : MonoBehaviour
{
    void Start()
    {
        // only used in Non-pic book, and not AR pages
        var wcc = WebCamController.getInstance();

        if(wcc.isRecording)
            wcc.resumeRecording();   
    }
}
