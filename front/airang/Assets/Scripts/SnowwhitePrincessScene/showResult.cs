using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showResult : MonoBehaviour
{
    public GameObject targetObj;
	public void SetActvieOnResult(string targetName)
	{
		targetObj.SetActive(true);
		Invoke("SetActiveOff", 2f);
	}
	public void SetActiveOff()
	{
		targetObj.SetActive(false);
	}
}
