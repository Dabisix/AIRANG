using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
	public Material skyboxMaterial;
	private void Update()
	{
		RenderSettings.skybox = skyboxMaterial;
	}
	// 첫번쨰 씬 FS000_Day_06_Sunless

	// 두번째 씬 FS017_Night_Cubemap
}
