using UnityEngine;

public class PrincessAnimationController : MonoBehaviour
{
    // �ִϸ��̼� ��Ʈ�ѷ� �����Ѵپƾƾ�
    Animator anim;
	public int page;

	private void Awake()
	{
		anim = GetComponentInChildren<Animator>();
	}

	private void Start()
	{
		switch (page)
		{
			case 1:
				SetIsHello(true);
				break;
			case 2:
				SetIsSadWalking(true);
				break;
		}
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
}
