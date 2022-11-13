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
        BookManager.getInstance().setCheckPoint(BookManager.getInstance().CurPage);
    }

    private void GoMainScene()
    {
        StartCoroutine(FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "MainScene"));
    }
}
