using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SquirrelEvent : MonoBehaviour
{
    public bool IsPointerOverUIObject(Vector2 touchPos)
    {
        PointerEventData eventDataCurrentPosition
            = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = touchPos;

        List<RaycastResult> results = new List<RaycastResult>();


        EventSystem.current
        .RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }

    void OnMouseDown()
    {
        AudioSource audio = this.gameObject.GetComponent<AudioSource>();
        //터치
        if (Input.touchCount > 0)
        {
            if (!IsPointerOverUIObject(Input.mousePosition))
            {
                //터치 처리
                Debug.Log("터치 안녕");
                //audio.clip = 
            }
        }

        //마우스 
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(Input.mousePosition))
            {
                Debug.Log("안녕");
            }
        }

    }
}
