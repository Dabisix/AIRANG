using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHomeButton : MonoBehaviour
{
    public MainMainCamera my_camera;
    public GameObject[] navigation;

    public void goCameraHome()
    {
        my_camera.MainMoveCamera(2);
    }

    public void setNavigationActive()
	{
        for(int i = 0; i < navigation.Length; ++i)
		{
            navigation[i].SetActive(true);
		}
	}
}
