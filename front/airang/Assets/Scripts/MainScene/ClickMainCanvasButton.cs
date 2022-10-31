using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMainCanvasButton : MonoBehaviour
{
    public MainMainCamera my_camera;
    public GameObject[] navigation;
    public void ClickBtnHome()
    {
        SetNavigationActive();
        MoveCamera(2);
    }
    public void ClickBtnLibrary()
    {
        MoveCamera(0);
        navigation[0].SetActive(false);
    }
    public void ClickBtnMyDesk()
    {
        MoveCamera(1);
        navigation[1].SetActive(false);
    }

    public void MoveCamera(int index)
    {
        my_camera.MainMoveCamera(index);
    }

    public void SetNavigationActive()
    {
        for (int i = 0; i < navigation.Length; ++i)
        {
            navigation[i].SetActive(true);
        }
    }
}
