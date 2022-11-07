using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Android;

//¹öÁî ID - 1


public class RecordingVoice : MonoBehaviour
{
    public Button button;

    private string _microphoneID;
    AudioClip recordClip;

    float timer;

    bool start;

    // Start is called before the first frame update
    void Start()
    {
        if(!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }

        _microphoneID = Microphone.devices[0];
        recordClip = GetComponent<AudioClip>();
    }

    public void OnClickStart()
    {
        recordClip = Microphone.Start(_microphoneID, false, 1000, 44100);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnClickEnd()
    {
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

        SavWav.Save("C:/recordingTest/test", recordClip);
    }
}
