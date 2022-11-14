using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicScript : MonoBehaviour
{
    AudioSource[] audioSource;  // 0: BGM, 1:click
    public Toggle toggle;

    private void Start()
    {
        audioSource = GameManager.getInstance().GetComponents<AudioSource>();

        // set sync toggle music
        if (audioSource[0].isPlaying)
        {
            toggle.isOn = false;
        }
    }

    public void toggleBGM()
    {
        foreach (var item in audioSource)
        {
            item.mute = toggle.isOn;
        }
    }

    //update : 클릭한 것이 button, toggle이면 소리 발생
    public void Update()
    {
       //클릭한 것 찾기
       if(TryGetTouchPosition(out Vector2 touchPosition))
        {
            //터치 없으면 돌아가기
            if (Input.touchCount == 0) return;
            
            //터치 있으면 
            Touch touch = Input.GetTouch(0);
            ray = Camera.main.ScreenPointToRay(touch.position);
            Physics.Raycast(ray, out hitobj);
            //해당 오브젝트에서 button, toggle 찾기

            //있으면 소리
        }
    }
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    private Vector2 touchPosition;
    private Ray ray;
    private RaycastHit hitobj;
}
