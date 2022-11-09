using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCheck : MonoBehaviour
{
	public Animator anim;
	public Transform animationTransform;
	public GameObject targetObj;
	public string animationName;

	private void Awake()
	{
		anim = animationTransform.GetComponent<Animator>();
	}

	private void Update()
	{
		AnimationChecktoActive(animationName);
	}
	public void AnimationChecktoActive(string animationName)
	{
		if (anim.GetCurrentAnimatorStateInfo(0).IsName(animationName))
		{
			// animationName이 지금 실행되고 있으면 타겟 오브젝트를 active시킬고얌
			targetObj.SetActive(true);
			Invoke("SetActiveOff", 2f);
		}
	}
	public void SetActiveOff()
	{
		targetObj.SetActive(false);
	}
}
