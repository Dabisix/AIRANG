using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuIconLoader : MonoBehaviour
{
    bool isOn = false;

    public GameObject[] buttons;

    // for button delay
    float m_LastPressTime;
    float m_PressDelay = 0.5f;

    public void toggleMenu()
    {
        // set button delay
        if (m_LastPressTime + m_PressDelay > Time.unscaledTime)
            return;
        m_LastPressTime = Time.unscaledTime;

        if (isOn)
            hideButtons();
        else
            showButtons();
        
        // toggle menus
        isOn = !isOn;
    }

    public void showButtons()
    {
        // show menu buttons
        foreach (var button in buttons)
            button.SetActive(true);
    }

    public void hideButtons()
    {
        // hide buttons
        foreach (var button in buttons)
            StartCoroutine(button.GetComponent<UIMovementHandler>().LerpBackObject());
    }
}
