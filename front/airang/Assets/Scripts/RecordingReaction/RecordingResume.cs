using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingResume : MonoBehaviour
{
    void Start()
    {
        WebCamController.getInstance().resumeRecording();   
    }
}
