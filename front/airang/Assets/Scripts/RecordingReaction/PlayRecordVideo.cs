using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video; // 비디오 플레이어 사용
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayRecordVideo : MonoBehaviour
{
    public VideoPlayer screenVideo; //직접 할당
    public VideoPlayer selfVideo;
    public Slider slider;
    public TextMeshProUGUI title;
    public Button backBtn;

    private void Start()
    {
        screenVideo.url = VideoManager.getInstance().CurVideo.ScreenUrl;
        selfVideo.url = VideoManager.getInstance().CurVideo.SelfUrl;
        title.text = VideoManager.getInstance().CurVideo.Title;
        Debug.Log(screenVideo.length);
        slider.minValue = 0;
        slider.maxValue = (float)screenVideo.clip.length;
    }

    private void Update()
    {
        slider.value = (float)screenVideo.time;
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

    public void clickBackBtn()
    {
        SceneManager.LoadScene("RecordListScene");
    }

    public void Share()
    {
		using (AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent")) 
		using (AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent")) {
			intentObject.Call<AndroidJavaObject>("setAction", intentObject.GetStatic<string>("ACTION_SEND"));
			intentObject.Call<AndroidJavaObject>("setType", "text/plain");
			intentObject.Call<AndroidJavaObject>("putExtra", intentObject.GetStatic<string>("EXTRA_SUBJECT"), "공유해줘 태일아");

			using (AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			using (AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity")) 
			using (AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via"))
			currentActivity.Call("startActivity", jChooser);
		}
    }
}
