using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    public void OnHomeClicked()
    {
        saveCurrentPage();
        GoMainScene();
    }

    private void saveCurrentPage()
    {
        // get File
        FileManager fb = FileManager.getInstance();
        SavedData tmp = fb.loadData();

        // save checkPoint
        BookManager bm = BookManager.getInstance();
        if(tmp.page_checkPoint.ContainsKey(bm.CurBook.BookId))
            tmp.page_checkPoint[bm.CurBook.BookId] = bm.CurPage;
        else
            tmp.page_checkPoint.Add(bm.CurBook.BookId, bm.CurPage);

        fb.saveData(tmp);
    }

    private void GoMainScene()
    {
        StartCoroutine(FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "MainScene"));
    }
}
