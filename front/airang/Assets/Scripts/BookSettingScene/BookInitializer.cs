using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInitializer : MonoBehaviour
{
    void Start()
    {
        BookManager.getInstance().InitBook();    
    }
}
