using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicScript : MonoBehaviour
{
    AudioSource audioSource;
    public Toggle toggle;

    private void Start()
    {
        audioSource = GameManager.getInstance().GetComponent<AudioSource>();

        // set sync toggle music
        if (audioSource.isPlaying)
        {
            toggle.isOn = false;
        }
            
    }

    public void toggleBGM()
    {
        audioSource.mute = toggle.isOn;
    }
}
