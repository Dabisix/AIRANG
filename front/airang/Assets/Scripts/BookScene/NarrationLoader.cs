using UnityEngine;
using UnityEngine.Networking;

public class NarrationLoader : MonoBehaviour
{
    new AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        downloadNarration();
    }

    public void downloadNarration()
    {
        // get Book Info
        var bookId = BookManager.getInstance().CurBook.BookId;
        var pageNum = BookManager.getInstance().CurPage;

        RESTManager.getInstance().getNarr(bookId, pageNum)
        .Then(res => {
             audio.clip = ((DownloadHandlerAudioClip)res.Request.downloadHandler).audioClip;
             audio.Play();
         }).Catch(err => {
             Debug.Log("Error " + err.Message);
         });
    }
}
