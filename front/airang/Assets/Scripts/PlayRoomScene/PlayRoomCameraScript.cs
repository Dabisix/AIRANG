using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class PlayRoomCameraScript : MonoBehaviour
{
    public Canvas[] uis;
    private bool isCapturing;
    public AudioClip captureAudioClip;
    public Canvas captureCanvas;
    public RawImage captureImage;

    private Texture2D captured_texture;
    private string fileName;

    public void CaptureScreen()
    {
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        fileName = "AIRANG-SCREENSHOT-" + timestamp + ".png";

        #if UNITY_IPHONE || UNITY_ANDROID
                CaptureScreenForMobile();
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
        /*uis[0].enabled = true;
        uis[1].enabled = true;
        captureCanvas.enabled = false;*/
        
    }

    private void CaptureScreenForPC(string fileName)
    {
        ScreenCapture.CaptureScreenshot("~/Downloads/" + fileName);
    }

    private void CaptureScreenForMobile()
    {
        captured_texture = ScreenCapture.CaptureScreenshotAsTexture();

        // show preview image
        captureImage.texture = captured_texture;
    }

    public void captureEnd()
    {
        // UI update
        uis[0].enabled = true;
        uis[1].enabled = true;
        captureCanvas.enabled = false;
        // capture end
        isCapturing = false;

        // cleanup
        Object.Destroy(captured_texture);
    }


    #region Result Canvas interactions
    public void shareResult()
    {
        string filePath = Path.Combine(Application.temporaryCachePath, fileName);
        File.WriteAllBytes(filePath, captured_texture.EncodeToPNG());

        new NativeShare().AddFile(filePath)
            .SetSubject("Airang").SetText("아이랑 한 컷")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
    }

    public void saveResult()
    {
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
        NativeGallery.SaveImageToGallery(captured_texture, albumName, fileName, (success, path) =>
        {
            Debug.Log(success);
            Debug.Log(path);
        });

        captureEnd();
    }
    #endregion
}
