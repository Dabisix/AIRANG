using Proyecto26;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class NarrationLoader : MonoBehaviour
{
    new AudioSource audio;

    GameObject narrSetting;

    public GameObject recordAlertObject;

    private void Awake()
    {
        // get narr setting Prefab
        GameObject narrSettingPrefab = Resources.Load<GameObject>("Prefabs/BookScene/NarrSettingCanvas");

        narrSettingPrefab.SetActive(false);
        narrSetting = Instantiate(narrSettingPrefab);
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        play();
    }

    public void openNarrSetting()
    {
        narrSetting.SetActive(true);
    }

    public void play()
    {
        // setting record
        FindObjectOfType<RecordParentVoice>().readyToRecord();

        int narr_setting = BookManager.getInstance().Narration;
        if (audio.isPlaying) audio.Stop();

        if (narr_setting == 0)
            return;
        else if (narr_setting == 1)
            downloadNarration();
        else
            loadParentNarr();
    }

    public void downloadNarration()
    {
        // get Book Info
        var bookId = BookManager.getInstance().CurBook.BookId;
        var pageNum = BookManager.getInstance().CurPage;

        // 기본 나레
        RESTManager.getInstance().getNarr(bookId, pageNum - 1)
            .Then(res => {
                audio.clip = ((DownloadHandlerAudioClip)res.Request.downloadHandler).audioClip;
                audio.Play();
            }).Catch(err => {
                GameManager.getInstance().alert("책 나레이션을 불러오는 중 \n문제가 발생하였습니다");
            });
    }

    public void loadParentNarr()
    {
        var fileUrl = "file://" + Application.persistentDataPath + "/" + BookManager.getInstance().CurBook.BookId.ToString() + "/" + BookManager.getInstance().CurPage.ToString() + ".wav"; //모바일용
        //var fileUrl = Application.persistentDataPath + "/" + BookManager.getInstance().CurBook.BookId.ToString() + "/" + BookManager.getInstance().CurPage.ToString() + ".wav"; //컴퓨터용
        
        var fileType = AudioType.WAV;

        RestClient.Get(new RequestHelper
        {
            Uri = fileUrl,
            DownloadHandler = new DownloadHandlerAudioClip(fileUrl, fileType)
        }).Then(res => {
            audio.clip = ((DownloadHandlerAudioClip)res.Request.downloadHandler).audioClip;
            audio.mute = false;
            audio.loop = false;
            audio.Play();
        }).Catch(err => {
            recordAlertObject.SetActive(true);
            Invoke("closeRecordAlert", 1.5f);            
        });
    }

    private void closeRecordAlert() {
        recordAlertObject.SetActive(false);
    }
}
