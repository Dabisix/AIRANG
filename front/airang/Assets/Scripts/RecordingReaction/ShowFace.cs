using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class ShowFace : MonoBehaviour
{
    Renderer renderer;
    WebCamDevice[] webCamDevices;
    WebCamTexture webCamTextures;

    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera)) {
            Permission.RequestUserPermission(Permission.Camera);
        }

        renderer = GetComponent<Renderer>();

        // find real camara
        webCamDevices = WebCamTexture.devices;
        if(webCamDevices.Length == 0) {
            Debug.Log("no Webcam");
            return;
        }

        for (int i = 0; i < webCamDevices.Length; i++) {
            // select front camera
            if (webCamDevices[i].isFrontFacing) {
                webCamTextures = new WebCamTexture(webCamDevices[i].name);
                break;
            }
        }

        if(webCamTextures != null) {
            webCamTextures.requestedFPS = 30;
            renderer.material.mainTexture = webCamTextures;
            webCamTextures.Play();
        }
    }

    private void OnDestroy()
    {
        // release camara
        if(webCamTextures != null) {
            webCamTextures.Stop();
            WebCamTexture.Destroy(webCamTextures);
        }
    }

    public void CameraOff()
    {
        if (webCamTextures != null) {
            webCamTextures.Stop();
            WebCamTexture.Destroy(webCamTextures);
        }
    }
}
