using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent (typeof (TextMeshProUGUI))]
public class TextSetter : MonoBehaviour
{
    public void setText(string text)
    {
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = text;
    }
}
