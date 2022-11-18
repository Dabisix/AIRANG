using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChracterEvent : MonoBehaviour
{
    public GameObject arang;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = arang.GetComponent<AudioSource>();
    }

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


        //터치
        if (Input.touchCount > 0)
        {
            if (!IsPointerOverUIObject(Input.mousePosition))
            {
                audioSource.loop = false;
                audioSource.Play();
                //터치 처리
                Debug.Log("안녕난아라이양");
            }
        }

        //마우스 
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(Input.mousePosition))
            {
                audioSource.loop = false;
                audioSource.Play();
                Debug.Log("안녕난아라이양");

            }
        }

    }
}
