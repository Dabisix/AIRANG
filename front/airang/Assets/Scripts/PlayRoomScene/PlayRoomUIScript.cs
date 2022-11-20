using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayRoomUIScript : MonoBehaviour
{
    public Canvas frameCanvas;
    public GameObject[] frames;
    public Canvas characterCanvas;
    public ToggleGroup characterToggles;
    public GameObject[] characterLists;
    public Canvas photoCanvas;
    private bool isFirst;

    public GameObject playRoomArScript;

    private int beforeFrameIndex;
    

    private void Awake()
    {
        closeAllFrame();
        closeCharacterMenuUI();
        closeFrameMenuUI();
        photoCanvas.enabled = false;
        isFirst = true;
    }
    public void openFrameMenuUI()
    {
        frameCanvas.enabled = true;
    }
    public void closeFrameMenuUI()
    {
        frameCanvas.enabled = false;
    }
    public void toggleFramePrefabs(int idx)
    {
        if(beforeFrameIndex > 0 && beforeFrameIndex == idx)
        {
            frames[idx - 1].SetActive(!frames[idx-1].activeSelf);
        }
        else
        {
            closeAllFrame();
            frames[idx - 1].SetActive(true);
        }
        beforeFrameIndex = idx;
    }
    public void closeAllFrame()
    {
        foreach(var obj in frames)
        {
            obj.SetActive(false);
        }
    }
    public void openCharacterMenuUI()
    {
        characterCanvas.enabled = true;
        if (isFirst)
        {
            unActiveAllCharacterPrefabs();
            characterLists[0].SetActive(true);
            isFirst = false;
        }
    }
    public void closeCharacterMenuUI()
    {
        characterCanvas.enabled = false;
    }
    public void changeCharacterPrefabs(int menuIndex)
    {
        unActiveAllCharacterPrefabs();
        characterLists[menuIndex].SetActive(true);
    }
    public void unActiveAllCharacterPrefabs()
    {
        foreach(var obj in characterLists)
        {
            obj.SetActive(false);
        }
    }

    public void goMain()
    {
        playRoomArScript.GetComponent<PlayRoomARScript>().ResetArPlane();
        SceneManager.LoadScene("MainScene");
    }
}
