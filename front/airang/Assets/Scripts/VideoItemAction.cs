using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VideoItemAction : MonoBehaviour, IPointerClickHandler
{
    private Video videoInfo;

    public Video Video
    {
        get => videoInfo;
        set => videoInfo = value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (videoInfo == null) return;

        VideoManager vm = VideoManager.getInstance();
        vm.CurVideo = videoInfo;
        Debug.Log("비디오 액션 : " + vm);
        vm.initVideo();
    }
}
