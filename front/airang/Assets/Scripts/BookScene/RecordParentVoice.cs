using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class RecordParentVoice : MonoBehaviour
{
    public GameObject startRecord;
    public GameObject stopRecord;

    private string _microphoneID;
    AudioClip recordClip;

    float timer;
    bool start;

    BookManager bookManager;

    // Start is called before the first frame update
    void Start()
    {
        bookManager = BookManager.getInstance();
    }

    public void readyToRecord()
    {
        if (bookManager.Narration == 1)
        {
            // dont need to use record
            startRecord.SetActive(false);
            stopRecord.SetActive(false);
            return;
        }

        // set permission
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            Permission.RequestUserPermission(Permission.Microphone);

        // active button
        startRecord.SetActive(true);
        stopRecord.SetActive(false);

        _microphoneID = Microphone.devices[0];
        recordClip = GetComponent<AudioClip>();
    }

    public void OnClickStart()
    {
        startRecord.SetActive(false);
        stopRecord.SetActive(true);
        Debug.Log("녹음이 시작됩니다.");
        recordClip = Microphone.Start(_microphoneID, false, 1000, 44100);
    }

    public void OnClickEnd()
    {
        startRecord.SetActive(true);
        stopRecord.SetActive(false);


        Debug.Log("녹음이 끝난다?");
        int lastTime = Microphone.GetPosition(null);

        if (lastTime == 0)
            return;
        else
        {
            Microphone.End(_microphoneID);

            float[] samples = new float[recordClip.samples];

            recordClip.GetData(samples, 0);

            float[] cutSamples = new float[lastTime];

            Array.Copy(samples, cutSamples, cutSamples.Length - 1);

            recordClip = AudioClip.Create("Notice", cutSamples.Length, 1, 44100, false);

            recordClip.SetData(cutSamples, 0);
        }

        // 책 아이디가 폴더이름이고, 그 아래 각 페이지 번호를 이름으로 녹음 파일 저장
        SavWav.Save(Application.persistentDataPath + "/" + bookManager.CurBook.BookId + "/" + bookManager.CurPage, recordClip);

        FindObjectOfType<NarrationLoader>().play();
    }

}
