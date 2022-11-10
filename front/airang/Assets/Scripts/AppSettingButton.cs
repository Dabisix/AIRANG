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
            GameObject settingPrefab = Resources.Load("Prefabs/MainScene/AppSettingBoardPrefab") as GameObject;
            settingPrefab.SetActive(false);
            appSettingBoardPrefab = Instantiate(settingPrefab);
        }
    }

    public void BookStart()
    {
        BookManager.getInstance().changeScene(true);
    }

    //openAppSetting
    public void openAppSettingPrefab()
    {
        appSettingBoardPrefab.SetActive(true); // at first
        appSettingBoardPrefab.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void goMainButton()
    {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "MainScene"));
    }
}
