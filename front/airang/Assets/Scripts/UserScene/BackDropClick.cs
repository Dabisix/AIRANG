using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackDropClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject canvas;

    public void OnPointerClick(PointerEventData eventData)
    {
        canvas.SetActive(false);
    }
}
