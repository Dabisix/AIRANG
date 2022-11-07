using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackDropClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    UIMovementHandler mMovementHandler;

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(mMovementHandler.LerpBackObject());   
    }
}
