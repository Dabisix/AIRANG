using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.Video;

public class RecordVideoListLoader : MonoBehaviour
{
    [SerializeField]
    GameObject videoItemPrefab;
    [SerializeField]
    Transform contentContainer;

    private void Start()
    {
        renderReactionVideos();
    }

    public void eraseAllBooks()
    {
        for (int i = 0; i < contentContainer.childCount; i++)
        {
            Destroy(contentContainer.GetChild(i).gameObject);
        }
    }

    // render at content in scrollView
    public void renderReactionVideos()
    {
        // 리액션 녹화 영상 가져오기
        //var fileUrl = "file://" + Application.persistentDataPath + "/" + BookManager.getInstance().CurBook.BookId.ToString() + "/" + BookManager.getInstance().CurPage.ToString() + ".wav"; //모바일용
        var fileUrl = Application.persistentDataPath + "/reaction" ; //컴퓨터용

        // 리액션 녹화 폴더 안 파일 확인
        DirectoryInfo d = new DirectoryInfo(fileUrl);
        FileInfo[] info = d.GetFiles("*.mp4");
        Debug.Log(info.Length);
        foreach (var file in info)
        {
            Debug.Log(file.Name);
        }

        //책 제목, 녹화한 날짜, 비디오 영상 할당
        for (int i = 0; i < info.Length; i++)
        {
            GameObject video = Instantiate(videoItemPrefab);
            GameObject title = video.transform.Find("Title").gameObject;
            VideoPlayer videoPlayer = video.transform.Find("VideoPlayer").gameObject.GetComponent<VideoPlayer>();
            title.GetComponent<TextMeshProUGUI>().text = info[i].Name;
            videoPlayer.url = Path.Combine("file://"+info[i].ToString());

            // set position
            video.transform.SetParent(contentContainer);
            video.transform.localScale = new Vector3(1.15f, 1.37f, 1);
            video.transform.localPosition = new Vector3(video.transform.position.x, video.transform.position.y, 0);
            video.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
