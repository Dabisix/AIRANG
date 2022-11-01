using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
	[SerializeField]
	[Range(0.0f, 10f)]
	private float fadeTime;
    private Image image;
	private void Awake()
	{
		image = GetComponent<Image>();

		//FadeIn. 배경의 알파값이 1에서 0으로 (화면이 점점 밝아지기)
		StartCoroutine(Fade(1,0));
	}


	private IEnumerator Fade(float start, float end)
	{
		float currentTime = 0.0f;
		float percent = 0.0f;

		while(percent < 1)
		{
			// fadeTime으로 나누어서 fadeTime 시간 동안 percent값이 0에서 1로 증가하도록
			currentTime += Time.deltaTime;
			percent = currentTime / fadeTime;

			// 앞파값을 start부터 end까지 fadeTime시간동안 변화시킨다.
			Color color = image.color;
			color.a = Mathf.Lerp(start, end, percent);
			image.color = color;

			yield return null;
		}
	}
}
