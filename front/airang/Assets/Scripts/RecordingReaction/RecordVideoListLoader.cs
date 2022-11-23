using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Linq;

public class RecordVideoListLoader : MonoBehaviour
{
    [SerializeField]
    GameObject videoItemPrefab;
    [SerializeField]
    Transform contentContainer;

    private List<Video> VideoList = new List<Video>();

    private void Start()
    {
        getVideos();
        renderReactionVideos(VideoList);
    }

    public void eraseAllBooks()
    {
        for (int i = 0; i < contentContainer.childCount; i++)
        {
            Destroy(contentContainer.GetChild(i).gameObject);
        }
    }

    private string GetAndroidExternalStoragePath()
    {
        if (Application.platform != RuntimePlatform.Android)
            return Application.persistentDataPath;

        var jc = new AndroidJavaClass("android.os.Environment");
        var path = jc.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory",
            jc.GetStatic<string>("DIRECTORY_DCIM"))
            .Call<string>("getAbsolutePath");
        return path;
    }

    public void getVideos()
    {
        // 리액션 녹화 영상 가져오기
        var fileUrl = GetAndroidExternalStoragePath() + "/airang";
        // var fileUrl = "file://" + Application.persistentDataPath; //모바일용
        // var fileUrl = Application.persistentDataPath + "/reaction"; //컴퓨터용

        // 리액션 녹화 폴더 안 파일 확인
        DirectoryInfo d = new DirectoryInfo(fileUrl);
        FileInfo[] info = d.GetFiles("*.mp4");

        string cam_file = "";
        string scr_file = "";

        foreach (var file in info)
        {
            if (!file.Name.Contains("airang_"))
                continue;

            if (cam_file == "") {
                if (file.Name.Contains("cam"))
                    cam_file = "file://" + file.ToString();
            } else
            {
                if (file.Name.Contains("scr"))
                    scr_file = "file://" + file.ToString();
                else
                    cam_file = "";
            }

            if(cam_file != "" && scr_file != "")
            {
                Debug.Log(file.Name);
                VideoList.Add(new Video(cam_file, scr_file, file.Name));
                cam_file = "";
                scr_file = "";
            }
        }
    }

    // render at content in scrollView
    public void renderReactionVideos(List<Video> videos)
    {

        //책 제목, 녹화한 날짜, 비디오 영상 할당
        for (int i = 0; i < videos.Count; i++)
        {

            RenderTexture renderTexture = new RenderTexture(256, 256, 16);
            renderTexture.Create();
            renderTexture.name = videos[i]+" Renderer";

            GameObject video = Instantiate(videoItemPrefab);
            video.transform.localScale = new Vector3(0.67f, 0.67f, 0.67f);
            video.GetComponent<VideoItemAction>().VideoInfo = videos[i];
            
            RawImage img = video.transform.Find("RawImage").gameObject.GetComponent<RawImage>();
            GameObject title = video.transform.Find("Title").gameObject;
           
            VideoPlayer videoPlayer = video.transform.Find("VideoPlayer").gameObject.GetComponent<VideoPlayer>();
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            videoPlayer.targetTexture = renderTexture;
            img.texture = renderTexture;

            title.GetComponent<TextMeshProUGUI>().text = videos[i].Title;
            videoPlayer.url = Path.Combine(videos[i].ScreenUrl);

            // set position
            video.transform.SetParent(contentContainer);
            video.transform.localPosition = new Vector3(video.transform.position.x, video.transform.position.y, 0);
            video.transform.localRotation = Quaternion.Euler(0, 0, 0);

            videoPlayer.Play();
            
            Texture2D texture = new Texture2D(256, 256, TextureFormat.RGBA32, false);
            RenderTexture.active = videoPlayer.targetTexture;
            texture.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            texture.Apply();

            //texture = ResampleAndCrop(texture, 256, 256);
            Debug.Log("이미지?? : "+Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f)));
            GameObject preview = video.transform.Find("Preview").gameObject;
            preview.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            
            videoPlayer.Pause();
            //videoPlayer.targetTexture = null;
            //RenderTexture.active = null;
        }
    }
}
