using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserLogin : MonoBehaviour
{
    public StartMainCamera my_camera;
    public DoorOpen doorOpen;

    // �ϴ� �ӽ÷� ���콺 Ŭ������ �� ī�޶� �������� �̵��ϰ� �غ���.
    private void OnMouseDown()
    {
        my_camera.StartMoveCamera(1);
        Invoke("openDoor", 2.7f);
    }

    private void openDoor()
    {
        doorOpen.openDoor();
    }
}