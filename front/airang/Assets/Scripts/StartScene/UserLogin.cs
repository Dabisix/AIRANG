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

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (anim != null)
            {
                // play Bounce but start at a quarter of the way though
                anim.Play("LoginButtonTouch", 0, 0.25f);
            }

            // 클릭하면 로그인 캔버스 활성화
            loginCanvas.SetActive(true);
        }

    }

}