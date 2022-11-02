using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenController : MonoBehaviour
{
	private void Start()
	{
	}

	public void SetQueenActive(bool isActive)
	{
		gameObject.SetActive(isActive);
	}
}
