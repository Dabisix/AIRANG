using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrController : MonoBehaviour
{
    int narr_setting = 1;

    public Toggle[] options;

    private void Start()
    {
        narr_setting = PlayerPrefs.GetInt("narr", 1);
        options[narr_setting].isOn = true;
    }

    public void toggleNarr(int narr)
    {
        narr_setting = narr;
    }

    public void apply()
    {
        BookManager.getInstance().Narration = narr_setting;
        PlayerPrefs.SetInt("narr", narr_setting);
       
        // play narration
        FindObjectOfType<NarrationLoader>().play();
    }
}
