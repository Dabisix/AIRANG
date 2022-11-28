using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class PlayRoomUIScript : MonoBehaviour
{
    public Canvas frameCanvas;
    public GameObject[] frames;
    public Canvas characterCanvas;
    public ToggleGroup characterToggles;
    public GameObject[] characterLists;
    public Canvas photoCanvas;
    private bool isFirst;
    private bool isSelfCam;

    public Canvas selfCharacterCanvas;

    public GameObject playRoomArScript;

    private int beforeFrameIndex;
    

    private void Awake()
    {
        closeAllFrame();
        closeCharacterMenuUI();
        closeFrameMenuUI();
        photoCanvas.enabled = false;
        selfCharacterCanvas.enabled = false;
        isFirst = true;
        isSelfCam = false;
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
        if (!isSelfCam)
        {
            characterCanvas.enabled = true;
            if (isFirst)
            {
                unActiveAllCharacterPrefabs();
                characterLists[0].SetActive(true);
                isFirst = false;
            }
        }
        else
        {
            selfCharacterCanvas.enabled = true;
        }
        
    }
    public void closeCharacterMenuUI()
    {
        if (!isSelfCam)
        {
            characterCanvas.enabled = false;
        }
        else
        {
            selfCharacterCanvas.enabled = false;
        }
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

    public void changeCamera()
    {
        isSelfCam = !isSelfCam;
        if (isSelfCam)
        {
            characterCanvas.enabled = false;
            selfCharacterCanvas.enabled = true;
        }
        else
        {
            characterCanvas.enabled = true;
            selfCharacterCanvas.enabled = false;
        }
    }

    public void goMain()
    {
        playRoomArScript.GetComponent<PlayRoomARScript>().ResetArPlane();
        SceneManager.LoadScene("MainScene");
    }
}
