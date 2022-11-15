using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    #region SINGLETON
    static VideoManager instance = null;

    // singleton Pattern implemented
    public static VideoManager getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // if object is corupted
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }
    #endregion

    private Video cur_video;

    public Video CurVideo
    {
        get => cur_video;
        set => cur_video = value;
    }

    public void initVideo()
    {
        changeScene(true);
    }

    public void changeScene(bool isFade = false)
    {
        Debug.Log("씬 바꿀거임");
        //if (isFade)
        //    StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, next_scene_name));
        //else
        //    SceneManager.LoadScene("PlayRecordVideo");
    }
}