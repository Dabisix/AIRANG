using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppSettingButtonFunction : MonoBehaviour
{
    // User Modify 
    GameObject userModifyPrefab;

    private void Start()
    {
        if (userModifyPrefab == null)
        {
            GameObject settingPrefab = Resources.Load("Prefabs/MainScene/UserModifyPrefab") as GameObject;
            settingPrefab.SetActive(false);
            userModifyPrefab = Instantiate(settingPrefab);
        }
    }

    public void activeUserModify()
    {
        StartCoroutine(GameObject.FindObjectOfType<UIMovementHandler>().LerpBackObject());
        setUserModifyActive();
    }

    private void setUserModifyActive()
    {
        userModifyPrefab.SetActive(true);
    }

    //로그아웃
    public void LogOut()
    {
        // PlayerPrefs에 저장한 모든 키 삭제
        PlayerPrefs.DeleteAll();
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "StartScene"));
    }
}
