using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UserSignup : MonoBehaviour
{
    public GameObject signupCanvas;
    void OnMouseDown()
    {
		if (!EventSystem.current.IsPointerOverGameObject())
		{
            // Ŭ���ϸ� ȸ������ ĵ���� Ȱ��ȭ
            signupCanvas.SetActive(true);
        }
    }
}
