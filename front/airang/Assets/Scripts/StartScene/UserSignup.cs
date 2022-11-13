using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserSignup : MonoBehaviour
{
    public GameObject signupCanvas;

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
                //터치 처리
                openSignUpCanvas();
            }
        }

        //마우스 
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(Input.mousePosition))
            {
                openSignUpCanvas();
            }
        }

    }

    public void openSignUpCanvas()
    {
        signupCanvas.SetActive(true);
    }

    public void closeSignUpCanvas()
    {
        signupCanvas.SetActive(false);
    }
}
