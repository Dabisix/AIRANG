using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HamSettingScript : MonoBehaviour
{
    public static GameObject hamSettingBoardPrefab;
    // Start is called before the first frame update
    private void Start()
    {
        if (hamSettingBoardPrefab == null)
        {
            GameObject settingPrefab = Resources.Load("Prefabs/HamSettingBoardPrefab") as GameObject;
            hamSettingBoardPrefab = Instantiate(settingPrefab);
            hamSettingBoardPrefab.SetActive(false);
        }
    }
    public void openAppSettingPrefab()
    {
        hamSettingBoardPrefab.SetActive(true);
    }

    public void closeAppSettingBoard()
    {
        hamSettingBoardPrefab.SetActive(false);
    }

    // changeScene
    public void changeScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }
}
