using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BookSettingManager : MonoBehaviour
{
    private bool lang = true; // kor : true, eng : false
    private bool parentVoice = false; // 기본 : false, 부모님 : true

    BookManager bookManager;

    public void toggleLang()
    {
        lang = !lang;
    }

    public void toggleNarr()
    {
        parentVoice = !parentVoice;
        Debug.Log(parentVoice ? "부모님" : "기본");
    }


    void Start()
    {
        bookManager = BookManager.getInstance();

        bookManager.InitBook();
    }

    public void readStart()
    {
        if (bookManager.CurBook == null) return;

        // 만약 부모님 나레이션 활성화 했을시
        if (parentVoice)
        {
            bookManager.ParentNarr = true;
        }

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
