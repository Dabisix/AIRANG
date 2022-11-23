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
using Unity.XR.CoreUtils;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

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
        // var fileUrl = Application.persistentDataPath + "/reaction"; // for PC

        // 리액션 녹화 폴더 안 파일 확인
        DirectoryInfo d = new DirectoryInfo(fileUrl);
        FileInfo[] info = d.GetFiles("*.mp4");

        string cam_file = "";
        string scr_file = "";

        foreach (var file in info)
        {
            if (!file.Name.Contains("airang_"))
                continue;

            if (cam_file == "" && file.Name.Contains("cam")) {
                cam_file = "file://" + file.ToString();
            } else
            {
                // cam and src file need to be in a row
                if (file.Name.Contains("scr"))
                    scr_file = "file://" + file.ToString();
                else
                    cam_file = "";
            }

            if(cam_file != "" && scr_file != "")
            {
                // add to rendering list
                VideoList.Add(new Video(cam_file, scr_file, file.Name));
                cam_file = scr_file = "";
            }
        }
    }

    // render at content in scrollView
    public void renderReactionVideos(List<Video> videos)
    {
        //책 제목, 녹화한 날짜, 비디오 영상 할당
        for (int i = 0; i < videos.Count; i++)
        {
            // make Prefab instance
            GameObject video_item = Instantiate(videoItemPrefab);
            video_item.transform.localScale = new Vector3(0.67f, 0.67f, 0.67f);
            video_item.GetComponent<VideoItemAction>().VideoInfo = videos[i];

            // set position
            video_item.transform.SetParent(contentContainer);
            video_item.transform.localPosition = new Vector3(video_item.transform.position.x, video_item.transform.position.y, 0);
            video_item.transform.localRotation = Quaternion.Euler(0, 0, 0);

            // set title
            string tmp_title = videos[i].Title.Substring(9);
            tmp_title = tmp_title.Substring(0, tmp_title.Length - 8);

            GameObject title = video_item.transform.Find("Title").gameObject;
            title.GetComponent<TextMeshProUGUI>().text = tmp_title;

            // get Thumbnail
            RawImage preview = video_item.transform.Find("Preview").gameObject.GetComponent<RawImage>();
            preview.texture = NativeGallery.GetVideoThumbnail(videos[i].ScreenUrl.Substring(8), -1, 60f); // substring for remove 'file://'
        }
    }
}
