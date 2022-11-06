using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSettingManager : MonoBehaviour
{
    private bool lang = true; // kor : true, eng : false

    public void setLang()
    {
        lang = !lang;
    }


    void Start()
    {
        
        BookManager.getInstance().InitBook();    
    }

    public void readStart()
    {
        BookManager.getInstance().changeScene(true);
    }
}
