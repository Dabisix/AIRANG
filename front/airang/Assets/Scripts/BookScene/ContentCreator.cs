using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ContentCreator : MonoBehaviour
{
    public void loadContentPrefab()
    {
        if (BookManager.getInstance() != null)
            m_Prefab = BookManager.getInstance().Content ?? null;
    }

    public void renderContent()
    {
        if (m_RendedObject != null)
            Destroy(m_RendedObject);
        else
        {
            m_RendedObject = Instantiate(m_Prefab, new Vector3(0, 0), new Quaternion(0, 0, 0, 0));
            getNarr(m_RendedObject);
            return;
        }
    }

    private void Start()
    {
        loadContentPrefab();
        renderContent();
        getNarr(m_RendedObject);
    }

    GameObject m_Prefab;
    GameObject m_RendedObject;

    public void getNarr(GameObject gameObject)
    {
        Debug.Log(gameObject.name);
        Debug.Log(BookManager.getInstance().ParentNarr ? "부모님" : "기본");
        Debug.Log(!BookManager.getInstance().NeedRecord ? "녹음 필요없음" : "녹음 필요");
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
