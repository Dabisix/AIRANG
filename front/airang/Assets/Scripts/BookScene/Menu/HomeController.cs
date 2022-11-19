using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    public void OnHomeClicked()
    {
        saveCurrentPage();
        BookManager.getInstance().goToMain();
    }

    private void saveCurrentPage()
    {
        // get File
        BookManager.getInstance().setCheckPoint(BookManager.getInstance().CurPage);
    }
}
