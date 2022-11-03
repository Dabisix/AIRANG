using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageChanger : MonoBehaviour {
   public void nextPage()
    {
        BookManager.getInstance().CurPage += 1;
        BookManager.getInstance().changeScene();
    }

    public void previousPage()
    {
        BookManager.getInstance().CurPage -= 1;
        BookManager.getInstance().changeScene();
    }
}
