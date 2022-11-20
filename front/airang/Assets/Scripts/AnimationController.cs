using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public bool isMove; //이동 여부
    public AnimationCurve animationCurve;
    public Transform animationTransform;

    public string animationName; //실행시킬 애니메이션 이름

    Vector3 animationStartPos;
    Vector3 animationTargetPos;

    public float timer;
    public float duration; //지속 시간동안 이동시킴
    public Vector3 vector;

    public Animator anim;
    public float speed; //애니메이션 스피드 (1 : 정상속도, ~ 0.1 : 느려짐, 0 : speed안씀)
    
    private void Awake()
    {
        anim = animationTransform.GetComponent<Animator>();
    }

    void Start()
    {
        // 만약 오브젝트가 움직일 거리가 있다면
        if (isMove)
        {
            anim.SetBool("is" + animationName, true);
            animationStartPos = animationTransform.position;
            animationTargetPos = animationStartPos + vector;
        }
        else
        {
            if(speed > 0)
                anim.speed = speed;
            anim.Play(animationName);
        }
    }

    void Update()
    {
        if (isMove)
        {
            timer += Time.deltaTime;
            float percentage = timer / duration;

            // 움직이는 경우에만??
            if (duration != 0)
            {
                animationTransform.position = Vector3.Lerp(animationStartPos, animationTargetPos, animationCurve.Evaluate(percentage));
            }

            // 해당 애니메이션의 위치가 목표 위치로 이동했을때 멈추게
            if (animationTransform.position == animationTargetPos)
            {
                anim.SetBool("is"+animationName, false);
                //anim.speed = 0;
            }
        }
    }

}
