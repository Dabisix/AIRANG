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
    // �ð� ������ SetActive���Ҿ���

    public void SetCharacterActive()
    {
        targetObj.SetActive(isActive);
    }
}
