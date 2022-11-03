using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
	public Material[] skyboxMaterials;
	public int defaultPage;
	public int page;
	void Start()
	{
		RenderSettings.skybox = skyboxMaterials[defaultPage];
	}

	private void Update()
	{
		RenderSettings.skybox = skyboxMaterials[page-1];
	}
	// Ã¹¹ø¤Š ¾À FS000_Day_06_Sunless

	// µÎ¹øÂ° ¾À FS017_Night_Cubemap
}
