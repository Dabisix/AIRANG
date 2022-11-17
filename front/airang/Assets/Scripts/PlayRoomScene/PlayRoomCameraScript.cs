using UnityEngine;
using System.Collections;
using System.IO;

public class PlayRoomCameraScript : MonoBehaviour
{
    public void ScreenShotCapture()
    {
        ScreenCapture.CaptureScreenshot(System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png");
    }

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

    private void CaptureScreenForPC(string fileName)
    {
        ScreenCapture.CaptureScreenshot("~/Downloads/" + fileName);
    }

    private void CaptureScreenForMobile(string fileName)
    {
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();

        // do something with texture
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
