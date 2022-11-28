using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicScript : MonoBehaviour
{
    public AudioSource audioSource;
    public Toggle toggle;   //true : muted

    private void Start()
    {
        audioSource = GameManager.getInstance().GetComponent<AudioSource>();
        // set sync toggle music
        if (audioSource.volume != 0f)
        {
            toggle.isOn = false;
        }
        else
        {
            toggle.isOn = true;
        }
    }

    public void toggleBGM()
    {
        if(toggle.isOn)
        {
            audioSource.volume = 0f;
        }
        else
        {
            audioSource.volume = 0.1f;
        }
    }
}
