using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
	private void Awake()
	{
		LoadingSceneManager.LoadScene("BookScene");
		Debug.Log("출력해주라.");
	}

}
