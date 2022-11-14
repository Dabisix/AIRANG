using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//start 버튼을 누르면 비디오가 재생되고,
//pause 버튼을 누르면 비디오가 멈추도록 해주는 VideoPlayer Controller

public class VideoPlayerManager : MonoBehaviour
{
    public VideoPlayer face;
    public VideoPlayer book;
 
    public void OnClickStart()
    {
        face.Play();
        book.Play();
    }

    public void OnClickPause()
    {
        face.Pause();
        book.Pause();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
