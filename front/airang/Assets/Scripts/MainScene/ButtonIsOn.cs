using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIsOn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Toggle>().isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
