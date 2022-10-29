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

    // �ϴ� �ӽ÷� ���콺 Ŭ������ �� ī�޶� �������� �̵��ϰ� �غ���.
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