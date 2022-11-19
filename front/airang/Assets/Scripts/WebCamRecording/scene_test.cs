using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scene_test : MonoBehaviour
{
    private void Start()
    {
        WebCamController.getInstance().WebCam.Play();
    }

    public void testEnd()
    {
        WebCamController.getInstance().stopRecording();
    }
}
