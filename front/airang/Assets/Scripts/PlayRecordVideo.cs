using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video; // 비디오 플레이어 사용

public class PlayRecordVideo : MonoBehaviour
{
    public VideoPlayer screenVideo; //직접 할당
    public VideoPlayer selfVideo;
    //VideoPlayer vp;

    private void Start()
    {
        //string path1 = "비디오 불러올 주소";
        //vp = GetComponent<VideoPlayer>();
        //vp.url = System.IO.Path.Combine(path1, "이름");
    }

    public void startVideo()
    {
        screenVideo.Play();
        selfVideo.Play();
    }

    public void pauseVideo()
    {
        screenVideo.Pause();
        selfVideo.Pause();
    }

    public void stopVideo()
    {
        screenVideo.Stop();
        selfVideo.Stop();
    }

    private const string subject = "원하는 제목";
    private const string body = "https://youtu.be/PYNvQIJ2cuM";

    public void Share()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
		using (AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent")) 
		using (AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent")) {
			intentObject.Call<AndroidJavaObject>("setAction", intentObject.GetStatic<string>("ACTION_SEND"));
			intentObject.Call<AndroidJavaObject>("setType", "text/plain");
			intentObject.Call<AndroidJavaObject>("putExtra", intentObject.GetStatic<string>("EXTRA_SUBJECT"), subject);
			intentObject.Call<AndroidJavaObject>("putExtra", intentObject.GetStatic<string>("EXTRA_TEXT"), body);
			using (AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			using (AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity")) 
			using (AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via"))
			currentActivity.Call("startActivity", jChooser);
		}
#endif
    }
}
