using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveIdx : MonoBehaviour
{
    public int activeId;

	private void Start()
	{
		activeId = 0;
	}

	public void SetActiveId(int id)
	{
		activeId = id;
	}
	public int GetActiveId()
	{
		return activeId;
	}
}
