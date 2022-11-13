using Proyecto26;
using UnityEngine;
using UnityEngine.Networking;

public class NarrationLoader : MonoBehaviour
{
    new AudioSource audio;

    GameObject narrSetting;

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
        downloadNarration();
    }

    public void openNarrSetting()
    {
        narrSetting.SetActive(true);
    }

    public void play()
    {
        audio.Play();
    }

    public void downloadNarration()
    {
        // get Book Info
        var bookId = BookManager.getInstance().CurBook.BookId;
        var pageNum = BookManager.getInstance().CurPage;

        Debug.Log(gameObject.name);
        Debug.Log(BookManager.getInstance().ParentNarr ? "부모님" : "기본");
        Debug.Log(!BookManager.getInstance().NeedRecord ? "녹음 필요없음" : "녹음 필요");

        // 기본 나레
        if (!BookManager.getInstance().ParentNarr)
        {
            RESTManager.getInstance().getNarr(bookId, pageNum - 1)
            .Then(res => {
                audio.clip = ((DownloadHandlerAudioClip)res.Request.downloadHandler).audioClip;
                audio.Play();
            }).Catch(err => {
                Debug.Log("Error " + err.Message);
            });
        }

        // 부모님 나레이션을 선택했고, 부모님 나레이션 녹음 파일이 있으면
        if (BookManager.getInstance().ParentNarr && !BookManager.getInstance().NeedRecord)
        {
            Debug.Log("부모님 나레이션 들려줄거임");
            AudioSource audio = gameObject.AddComponent<AudioSource>();
            // TODO
            var fileUrl = "file://" + Application.persistentDataPath + "/" + BookManager.getInstance().CurBook.BookId.ToString() + "/" + BookManager.getInstance().CurPage.ToString() + ".wav"; //모바일용
            //var fileUrl = Application.persistentDataPath + "/" + BookManager.getInstance().CurBook.BookId.ToString() + "/" + BookManager.getInstance().CurPage.ToString() + ".wav"; //컴퓨터용
            Debug.Log("파일 경로 : " + fileUrl);
            var fileType = AudioType.WAV;

            RestClient.Get(new RequestHelper
            {
                Uri = fileUrl,
                DownloadHandler = new DownloadHandlerAudioClip(fileUrl, fileType)
            }).Then(res => {
                Debug.Log("성공???");
                audio.clip = ((DownloadHandlerAudioClip)res.Request.downloadHandler).audioClip;
                audio.mute = false;
                audio.loop = false;
                audio.Play();
            }).Catch(err => {
                Debug.Log("Error " + err.Message);
            });
        }

    }

}
