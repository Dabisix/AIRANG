using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoItemAction : MonoBehaviour, IPointerClickHandler
{
    private Video videoInfo;

    public Video VideoInfo
    {
        get => videoInfo;
        set => videoInfo = value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(videoInfo.Title);
        Debug.Log(videoInfo.ScreenUrl);
        Debug.Log(videoInfo.SelfUrl);
        Debug.Log("비디오 클릭할거야~");
        if (videoInfo == null)
        {
            Debug.Log("널");
            return;
        }

        VideoManager vm = VideoManager.getInstance();
        vm.CurVideo = videoInfo;
        vm.initVideo();
    }
}
