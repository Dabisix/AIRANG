using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public AnimationCurve animationCurve;
    public Transform animationTransform;
    public string animationName; //실행시킬 애니메이션 이름

    Vector3 animationStartPos;
    Vector3 animationTargetPos;

    public float timer;
    public float duration; //지속 시간동안 이동시킴
    public Vector3 vector;
    public Animator anim;
    private void Awake()
    {
        anim = animationTransform.GetComponent<Animator>();
    }

    private void Start()
    {
        animationStartPos = animationTransform.position;
        animationTargetPos = animationStartPos + vector;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float percentage = timer / duration;

        // 움직이는 경우에만??
        if (duration != 0)
        {
            animationTransform.position = Vector3.Slerp(animationStartPos, new Vector3(-0.5f,1.4f,0), animationCurve.Evaluate(percentage));
        }

        // 해당 애니메이션의 위치가 목표 위치로 이동했을때 멈추게
        if (animationTransform.position == animationTargetPos)
        {
            Debug.Log("도착함");
            anim.speed = 0;
        }
    }
}
