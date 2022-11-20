using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionBtn : MonoBehaviour
{
    public GameObject beforeBtn;
    public GameObject nextBtn;
    public GameObject[] guide;

    int index = 0;

    private void Start()
    {
        if (index == 0)
        {
            beforeBtn.SetActive(false);
        }
        if (index == 4)
        {
            nextBtn.SetActive(false);
        }
    }

    private void Update()
    {
        Debug.Log("지금 인덱스 : "+index);
        if (index == 0)
        {
            beforeBtn.SetActive(false);
        }
        else
        {
            beforeBtn.SetActive(true);
        }
        if(index == 4)
        {
            nextBtn.SetActive(false);
        }
        else
        {
            nextBtn.SetActive(true);
        }
    }

    public void clickBeforeBtn()
    {
        if(index != 0)
        {
            guide[index].SetActive(false);
            index--;
            guide[index].SetActive(true);
        }
    }

    public void clickNextBtn()
    {
        if(index != 4)
        {
            guide[index].SetActive(false);
            index++;
            guide[index].SetActive(true);
        }
    }

    public void goToHome()
    {
        SceneManager.LoadScene("MainScene");
    }
}
