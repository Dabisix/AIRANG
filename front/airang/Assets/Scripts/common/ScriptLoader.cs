using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScriptLoader : MonoBehaviour
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

    /*public string LastClickedWord;

    private void Update()
    {
        // check clicked word
        if (Input.GetMouseButtonDown(0))
        {
            var wordIndex = TMP_TextUtilities.FindIntersectingWord(script_text, Input.mousePosition, null);

            if (wordIndex != -1)
            {
                LastClickedWord = script_text.textInfo.wordInfo[wordIndex].GetWord();

                Debug.Log("Clicked on " + LastClickedWord);
            }
        }
    }*/
}
