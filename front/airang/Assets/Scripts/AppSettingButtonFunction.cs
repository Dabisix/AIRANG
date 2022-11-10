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

    //로그아웃
    public void LogOut()
    {
        // PlayerPrefs에 저장한 모든 키 삭제
        PlayerPrefs.DeleteAll();
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "StartScene"));
    }
}
