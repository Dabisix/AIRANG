using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessAnimationController : MonoBehaviour
{
    // �ִϸ��̼� ��Ʈ�ѷ� �����Ѵپƾƾ�
    Animator anim;

	private void Awake()
	{
		anim = GetComponentInChildren<Animator>();
	}

	private void Start()
	{
		SetIsSadWalking(true);
	}

	public void SetIsSadWalking(bool isSadWalking)
	{
		anim.SetBool("isSadWalking", isSadWalking);
	}
	public void SetIsCryingAfterWalking(bool isCryingAfterWalking)
	{
		anim.SetBool("isCryingAfterWalking", isCryingAfterWalking);
	}
	public void SetIsCrying(bool isCrying)
	{
		anim.SetBool("isCrying", isCrying);
	}

	public void SetIsHello(bool isHello)
	{
		anim.SetBool("isHello", isHello);
	}
	private void Update()
	{
		
	}
}
