using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetBeforeAR : MonoBehaviour
{
    public GameObject BeforeARObj;
    public TextMeshProUGUI guideAR;
    

    public void GetPlane()
	{
        BeforeARObj.SetActive(false);
        guideAR.text = "";

    }
}
