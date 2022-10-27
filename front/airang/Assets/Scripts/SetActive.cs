using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public MainCamera my_camera;
    public DoorOpen doorOpen;

    // 일단 임시로 마우스 클릭했을 때 카메라가 집안으로 이동하게 해보장.
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
