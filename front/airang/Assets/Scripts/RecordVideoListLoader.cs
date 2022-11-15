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

        //RawImage display;

        //WebCamDevice[] webCamDevices;
        //WebCamTexture webCamTextures;

        //display = GetComponent<RawImage>();

        //// find real camara
        //webCamDevices = WebCamTexture.devices;
        //webCamTextures = new WebCamTexture(webCamDevices[i].name);

        //webCamTextures.requestedFPS = 30;
        //display.texture = webCamTextures;

        //책 제목, 녹화한 날짜, 비디오 영상 할당
        for (int i = 0; i < info.Length; i++)
        {
            RenderTexture renderTexture = new RenderTexture(256, 256, 16);
            renderTexture.Create();
            renderTexture.name = info[i].Name+" Renderer";

            GameObject video = Instantiate(videoItemPrefab);
            RawImage img = video.transform.Find("RawImage").gameObject.GetComponent<RawImage>();
            GameObject title = video.transform.Find("Title").gameObject;
           
            VideoPlayer videoPlayer = video.transform.Find("VideoPlayer").gameObject.GetComponent<VideoPlayer>();
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            videoPlayer.targetTexture = renderTexture;
            img.texture = renderTexture;

            title.GetComponent<TextMeshProUGUI>().text = info[i].Name;
            videoPlayer.url = Path.Combine("file://" + info[i].ToString());

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
