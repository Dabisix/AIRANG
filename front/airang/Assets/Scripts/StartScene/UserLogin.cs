using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserLogin : MonoBehaviour
{
    private Animator anim;
    public GameObject loginCanvas;
    

    void Start()
    {
        anim = GetComponent<Animator>();
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

    private void OnMouseDown()
    {
        //마우스 
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(Input.mousePosition))
            {
                if (anim != null)
                {
                    // play Bounce but start at a quarter of the way though
                    anim.Play("LoginButtonTouch", 0, 0.25f);
                }

                // active when user click this button
                loginCanvas.SetActive(true);
            }
        }

        //터치
        if (Input.touchCount > 0)
        {
            if (!IsPointerOverUIObject(Input.mousePosition))
            {
                //터치 처리
                if (anim != null)
                {
                    // play Bounce but start at a quarter of the way though
                    anim.Play("LoginButtonTouch", 0, 0.25f);
                }

                // active when user click this button
                loginCanvas.SetActive(true);
            }
        }

    }

}