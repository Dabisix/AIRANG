using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isAnimatiomEnded : MonoBehaviour
{
    Animator anim;
    public string animName;
    // Start is called before the first frame update

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animationTransition(animName);
    }

    void animationTransition(string animName) 
	{
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(animName))
        {
            anim.SetBool("is" + animName, false);
        }
    }
}
