using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMainCanvasButton : MonoBehaviour
{
    public MainMainCamera my_camera;
    public GameObject[] navigation;

    public void moveCamera(int index)
    {
        my_camera.MainMoveCamera(index);
    }

    public void setNavigationActive()
    {
        for (int i = 0; i < navigation.Length; ++i)
        {
            navigation[i].SetActive(true);
        }
    }
}
