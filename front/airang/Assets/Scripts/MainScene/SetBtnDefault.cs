using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBtnDefault : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle[] btnToggle;
    void Start()
    {
         btnToggle[0].isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
