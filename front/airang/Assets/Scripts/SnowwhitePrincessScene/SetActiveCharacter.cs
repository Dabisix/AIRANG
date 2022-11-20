using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveCharacter : MonoBehaviour
{
    public float time;
    public bool isActive;
    public GameObject targetObj;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetCharacterActive", time);
    }
    // 시간 지나면 SetActive해죠야지

    public void SetCharacterActive()
    {
        targetObj.SetActive(isActive);
    }
}
