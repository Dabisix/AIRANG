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

        BookManager bm = BookManager.getInstance();
        tmp.page_checkPoint.TryAdd(bm.CurBook.BookId, bm.CurPage);

        fb.saveData(tmp);
    }

    private void GoMainScene()
    {
        StartCoroutine(FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "MainScene"));
    }
}
