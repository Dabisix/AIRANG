using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookPrefabButtonScripts : MonoBehaviour
{
    // changeScene
    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //go before page
    public void goBack()
    {
        BookManager.getInstance().CurPage -= 1;
        BookManager.getInstance().changeScene();
    }

    //go next page
    public void goNext()
    {
        var bm = BookManager.getInstance();

        //지금 녹음 상태일때 
        if(bm.Narration == 2)
        {
            // record end
            FindObjectOfType<RecordParentVoice>().OnClickEnd();
        }

        bm.CurPage += 1;
        bm.changeScene();
    }
}
