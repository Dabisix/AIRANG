using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppSettingButton : MonoBehaviour
{
    public static GameObject appSettingBoardPrefab;

    private void Start()
    {
        if(appSettingBoardPrefab == null)
        {
            GameObject settingPrefab = Resources.Load("Prefabs/AppSettingBoardPrefab") as GameObject;
            appSettingBoardPrefab = Instantiate(settingPrefab);
            appSettingBoardPrefab.SetActive(false);
        }
    }

    public void BookStart()
    {
        BookManager.getInstance().changeScene(true);
    }

    //openAppSetting
    public void openAppSettingPrefab()
    {
        appSettingBoardPrefab.SetActive(true);
    }
    public void closeAppSettingPrefab()
    {
        appSettingBoardPrefab.SetActive(false);
    }
    public void goMainButton()
    {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "MainScene"));
    }
}
