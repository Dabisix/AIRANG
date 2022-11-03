using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenAnimationController : MonoBehaviour
{ // 애니메이션 컨트롤러 시작한다아아아
	Animator anim;
	public int page;
	private void Awake()
	{
		anim = GetComponentInChildren<Animator>();
	}

	void Start()
	{
		switch (page)
		{
			case 1:
				SetIsRightTurn(true);
				break;
			case 2:
				SetIsSitting(true);
				break;
		}
	}
	public void SetIsRightTurn(bool isRightTurn)
	{
		anim.SetBool("isRightTurn", isRightTurn);
	}
	public void SetIsSitting(bool isSitting)
	{
		anim.SetBool("isSitting", isSitting);
	}
}
