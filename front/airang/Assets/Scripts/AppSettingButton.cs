using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        BookManager.getInstance().changeScene();
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
}
