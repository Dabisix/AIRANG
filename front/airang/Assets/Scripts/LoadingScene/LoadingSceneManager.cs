using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingSceneManager : MonoBehaviour
{
    public TextMeshProUGUI Percent;
    public static string nextScene;



    [SerializeField]
    Image progressBar;

    // Start is called before the first frame update
    void Start()
    {
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadSceneAsync("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return new WaitForFixedUpdate();
            timer += Time.deltaTime;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    if (nextScene == "BookScene")
                    {
                        yield return StartCoroutine(BookManager.getInstance().loadContents());
					}
					else
					{
                        yield break;
					}

                }
            }
        }
    }
}
