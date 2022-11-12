using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
    public TextMeshProUGUI script_text;

    void Start()
    {
        loadScript();
    }

    public void loadScript()
    {
        script_text.text = BookManager.getInstance().Script;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
