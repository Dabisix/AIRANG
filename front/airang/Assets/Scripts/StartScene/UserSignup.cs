using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserSignup : MonoBehaviour
{
    public GameObject signupCanvas;

    void OnMouseDown()
    {
		if (!EventSystem.current.IsPointerOverGameObject())
		{
            // 클릭하면 회원가입 캔버스 활성화
            openSignUpCanvas();
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
