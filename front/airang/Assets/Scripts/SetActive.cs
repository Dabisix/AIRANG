using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public MainCamera my_camera;
    public DoorOpen doorOpen;

    // �ϴ� �ӽ÷� ���콺 Ŭ������ �� ī�޶� �������� �̵��ϰ� �غ���.
    private void OnMouseDown()
    {
        my_camera.MoveCamera(1);
        Invoke("openDoor", 2f);
    }

    private void openDoor()
	{
        doorOpen.openDoor();
    }
}
