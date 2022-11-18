using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRoomUIScript : MonoBehaviour
{
    public Canvas frameCanvas;
    public GameObject[] frames;
    public Canvas characterCanvas;
    
    private int beforeFrameIndex;

    private void Awake()
    {
        closeAllFrame();
        closeCharacterMenuUI();
        closeFrameMenuUI();
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
    }
    public void closeCharacterMenuUI()
    {
        characterCanvas.enabled = false;
    }
    public void changeCharacterPrefabs(int menuIndex)
    {
        
    }
}
