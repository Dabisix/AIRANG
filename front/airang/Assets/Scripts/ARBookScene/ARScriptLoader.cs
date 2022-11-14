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
        else if (BookManager.getInstance().ARType == 3)
        {
            AR_3 aR_3 = arSessionOrigin.GetComponent<AR_3>();
            aR_3.enabled = true;

            Debug.Log("3 enabled");
        }
        else if (BookManager.getInstance().ARType == 4)
        {
            AR_4 aR_4 = arSessionOrigin.GetComponent<AR_4>();
            aR_4.enabled = true;

            Debug.Log("4 enabled");
        }
        else if (BookManager.getInstance().ARType == 5)
        {
            AR_5 aR_5 = arSessionOrigin.GetComponent<AR_5>();
            aR_5.enabled = true;

            Debug.Log("5 enabled");
        }
        else if (BookManager.getInstance().ARType == 6)
        {
            AR_6 aR_6 = arSessionOrigin.GetComponent<AR_6>();
            aR_6.enabled = true;

            Debug.Log("6 enabled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
