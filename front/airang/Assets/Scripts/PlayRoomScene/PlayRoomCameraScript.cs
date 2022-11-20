using UnityEngine;
using System.Collections;
using System.IO;

public class PlayRoomCameraScript : MonoBehaviour
{
    public Canvas[] uis;
    private bool isCapturing;
    public AudioClip captureAudioClip;
    public Canvas captureCanvas;

    public void CaptureScreen()
    {
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = "AIRANG-SCREENSHOT-" + timestamp + ".png";

        #if UNITY_IPHONE || UNITY_ANDROID
                CaptureScreenForMobile(fileName);
#else
                CaptureScreenForPC(fileName);
#endif
    }

    public void clickCameraButton()
    {
        if (!isCapturing)
        {
            StartCoroutine(MakeScreenShot());
            StopCoroutine(MakeScreenShot());
        }
    }

    public IEnumerator MakeScreenShot()
    {
        isCapturing = true;
        foreach (var ui in uis)
        {
            ui.enabled = false;
        }

        yield return new WaitForEndOfFrame();
        gameObject.transform.GetComponent<AudioSource>().PlayOneShot(captureAudioClip);
        CaptureScreen();
        captureCanvas.enabled = true;
        yield return new WaitForSecondsRealtime(1f);
        uis[0].enabled = true;
        uis[1].enabled = true;
        captureCanvas.enabled = false;
        isCapturing = false;
    }

    private void CaptureScreenForPC(string fileName)
    {
        ScreenCapture.CaptureScreenshot("~/Downloads/" + fileName);
    }

    private void CaptureScreenForMobile(string fileName)
    {
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();

        // do something with texture

        //저장
        NativeGallery.Permission permission = NativeGallery.CheckPermission(NativeGallery.PermissionType.Write);
        if (permission == NativeGallery.Permission.Denied)
        {
            if (NativeGallery.CanOpenSettings())
            {
                NativeGallery.OpenSettings();
            }
        }
        string albumName = "AIRANG";
        NativeGallery.SaveImageToGallery(texture, albumName, fileName, (success, path) =>
        {
            Debug.Log(success);
            Debug.Log(path);
        });

        // cleanup
        Object.Destroy(texture);
    }
}
