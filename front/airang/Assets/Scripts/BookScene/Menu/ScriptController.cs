using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScriptController : MonoBehaviour
{
    public TextMeshProUGUI script_text;
    public GameObject langButton;

    private BookManager bm;

    // sprite for button
    public Sprite Kor;
    public Sprite Eng;

    void Start()
    {
        bm = BookManager.getInstance();

        // first Load Scripts
        loadScript();

        // set button image
        updateImage();
    }

    public void loadScript()
    {
        script_text.text = bm.Script;
    }

    public void updateImage()
    {
        if (bm.Lang) // KOR
            langButton.GetComponent<Image>().sprite = Kor;
        else // ENG
            langButton.GetComponent<Image>().sprite = Eng;
    }   

    public void OnLangClicked()
    {
        // Toggle lanuages
        bm.Lang = !bm.Lang;

        updateImage();
        loadScript();
    }
}
