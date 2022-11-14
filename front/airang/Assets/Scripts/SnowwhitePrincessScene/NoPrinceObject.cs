using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoPrinceObject : MonoBehaviour
{
    public GameObject NoPrinceResult;
    public TextMeshProUGUI ResultText;

    public void ClickNoPrince()
    {
        if (gameObject.name == "P_Hunter")
        {
            ResultText.text = "사냥꾼은 백설공주를 구해줄 수 없어!";
            NoPrinceResult.SetActive(true); 
            Invoke("SetActiveOff", 2f);
        }
        if (gameObject.name == "P_BadQueen")
        {
            ResultText.text = "사악한 왕비는 백설공주를 구해줄 수 없어!";
            NoPrinceResult.SetActive(true);
            Invoke("SetActiveOff", 2f);
        }

    }
    public void SetActiveOff()
    {
        NoPrinceResult.SetActive(false);
    }
}
