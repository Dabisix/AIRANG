using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserLogin : MonoBehaviour
{
    public StartMainCamera my_camera;
    public DoorOpen doorOpen;
    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // 일단 임시로 마우스 클릭했을 때 카메라가 집안으로 이동하게 해보장.
    private void OnMouseDown()
    {
        if (anim != null)
        {
            // play Bounce but start at a quarter of the way though
            anim.Play("LoginButtonTouch", 0, 0.25f);
        }
        my_camera.StartMoveCamera(1);
        Invoke("openDoor", 2.7f);
    }

    private void openDoor()
    {
        doorOpen.openDoor();
    }
}