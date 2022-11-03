using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveScript : MonoBehaviour
{
    public AnimationCurve animationCurve;   //애니메이션 움직임 정도 (빠르게, 천천히)
    public Transform objectTransform;   //움직일 object

    private Vector3 startPos; //시작 위치
    public Vector3 targetPos; //목표 위치
    
    // Start is called before the first frame update
    float timer = 0.0f;
    public float duration = 15f; //지속 시간동안 이동시킴

    // Start is called before the first frame update
    void Start()
    {
       startPos = objectTransform.position;    //현재 위치
        // startPos와 localPosition 비교하여 targetPos값 변경
        targetPos.x = startPos.x - transform.localPosition.x + targetPos.x;
        targetPos.y = startPos.y - transform.localPosition.y + targetPos.y;
        targetPos.z = startPos.z - transform.localPosition.z + targetPos.z;
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float percentage = timer / duration;
        objectTransform.position = Vector3.Lerp(startPos, targetPos, animationCurve.Evaluate(percentage));
        if (objectTransform.position == targetPos)  //위치 도착하면
        {
            Animator animator = objectTransform.GetComponent<Animator>();
            animator.speed = 0.0f;  //애니메이션 멈추게
            animator.Play("Idle_A");    //기본 애니메이션으로 변경(점프하면서 멈추는 것 방지용)   
        }
    }
}
