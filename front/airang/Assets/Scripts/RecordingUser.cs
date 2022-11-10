using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;  //필수 선언
using UnityEngine.UI;

public class RecordingUser : MonoBehaviour
{
    WebCamTexture cam;
    public RawImage camView;

    void Start()

    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
    }

    public void CameraOn()
    {
        if(WebCamTexture.devices.Length == 0)
        {
            Debug.Log("!!------no camera------!!");
            return;
        }

        WebCamDevice[] devices = WebCamTexture.devices;
        int selectedCamera = -1;

        for(int i = 0; i < devices.Length; i++)
        {
            if(devices[i].isFrontFacing)
            {
                selectedCamera = i;
                break;
            }
        }

        if(selectedCamera != -1)
        {
            cam = new WebCamTexture(devices[selectedCamera].name);
            cam.requestedFPS = 30;

            camView.texture = cam;

            cam.Play();
        }
    }

    public void CameraOff()
    {
        if(cam != null)
        {
            cam.Stop();
            WebCamTexture.Destroy(cam);
            cam = null;
        }
    }
}
