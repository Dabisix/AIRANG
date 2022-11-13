using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookPrefabButtonScripts : MonoBehaviour
{
    public GameObject recordAlertObject;

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
        bool isNext = false;
        if (BookManager.getInstance().NeedRecord) //녹음하고 있는 중임
        {
            // 폴더 이름 확인하기
            if (!BookManager.getInstance().checkRecordVoice()) //false면 폴더가 있음
            {
                DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath + "/" + BookManager.getInstance().CurBook.BookId);
                foreach (FileInfo File in di.GetFiles())
                {
                    // 현재 페이지를 녹음했다면
                    if (BookManager.getInstance().CurPage.ToString() + ".wav" == File.Name)
                    {
                        isNext = true;
                        break;
                    }
                }
                if (isNext) // 녹음 했으니 다음페이지로 가라
                {
                    BookManager.getInstance().CurPage += 1;
                    BookManager.getInstance().changeScene();
                }
                else
                {
                    recordAlertObject.SetActive(true);
                    Invoke("closeRecordAlert", 1.5f);
                }

            }
            else // 폴더가 아예 없으면 무조건 녹음해라
            {
                Debug.Log("폴더 자체도 없다");
                recordAlertObject.SetActive(true);
                Invoke("closeRecordAlert", 1.5f);
            }
        }
        else
        {
            BookManager.getInstance().CurPage += 1;
            BookManager.getInstance().changeScene();
        }
    }

    void closeRecordAlert()
    {
        recordAlertObject.SetActive(false);
    }

}
