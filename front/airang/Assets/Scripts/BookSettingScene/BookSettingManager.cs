using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSettingManager : MonoBehaviour
{
    private bool lang = true; // kor : true, eng : false

    BookManager bookManager;

    public void toggleLang()
    {
        lang = !lang;
    }


    void Start()
    {
        bookManager = BookManager.getInstance();

        bookManager.InitBook();
    }

    public void readStart()
    {
        if (bookManager.CurBook == null) return;

        // write read record to server
        RESTManager.getInstance().Put("book/log/" + bookManager.CurBook.BookId, null);

        // language and get checkpoint
        bookManager.CurBook.Lang = lang;
        bookManager.CurPage = 1;
        bookManager.changeScene(true);
    }

    public void getCheckPoint()
    {

    }
}
