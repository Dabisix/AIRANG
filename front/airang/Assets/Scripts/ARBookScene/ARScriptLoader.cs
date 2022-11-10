using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARScriptLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject arSessionOrigin = GameObject.Find("AR Session Origin");

        if(BookManager.getInstance().ARType == 1)
        {
            AR_1 aR_1 = arSessionOrigin.GetComponent<AR_1>();
            aR_1.enabled = true;

            Debug.Log("1 enabled");
        } else if(BookManager.getInstance().ARType == 2)
        {
            AR_2 aR_2 = arSessionOrigin.GetComponent<AR_2>();
            aR_2.enabled = true;

            Debug.Log("2 enabled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
