using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppSettingButtonFunction : MonoBehaviour
{
    // closeAppSetting
    public void closeAppSettingBoard()
    {
        AppSettingButton.appSettingBoardPrefab.SetActive(false);
    }

    // changeScene
    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
