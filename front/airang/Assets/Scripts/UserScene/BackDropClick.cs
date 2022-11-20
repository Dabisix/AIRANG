using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackDropClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    UIMovementHandler mMovementHandler;

    // for button delay
    float m_LastPressTime;
    float m_PressDelay = 0.5f;

    public void OnPointerClick(PointerEventData eventData)
    {
        // set button delay
        if (m_LastPressTime + m_PressDelay > Time.unscaledTime)
            return;
        m_LastPressTime = Time.unscaledTime;

        StartCoroutine(mMovementHandler.LerpBackObject());   
    }
}
