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

		//FadeIn. ����� ���İ��� 1���� 0���� (ȭ���� ���� �������)
		StartCoroutine(Fade(1,0));
	}


	private IEnumerator Fade(float start, float end)
	{
		float currentTime = 0.0f;
		float percent = 0.0f;

		while(percent < 1)
		{
			// fadeTime���� ����� fadeTime �ð� ���� percent���� 0���� 1�� �����ϵ���
			currentTime += Time.deltaTime;
			percent = currentTime / fadeTime;

			// ���İ��� start���� end���� fadeTime�ð����� ��ȭ��Ų��.
			Color color = image.color;
			color.a = Mathf.Lerp(start, end, percent);
			image.color = color;

			yield return null;
		}
	}
}
