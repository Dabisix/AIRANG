using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayRoomModelScript : MonoBehaviour
{
    private bool isMove;        //움직이는 중인가?

    private Animator anim;      //애니메이션
    private bool hasIntegerId;  //모델 타입 구분

    private Transform animationTransform;   //현재 모델 타입

    private float waitTime;         //움직이기까지 남은 시간
    private float timer = 1f;
    private Vector3 startPos;   //시작 지점
    private Vector3 targetPos;  //도착지점
    private float duration; //몇초동안 움직일 것인가

    private void Awake()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        animationTransform = gameObject.transform;
    }
    private void Start()
    {
        animationIdle();
        waitTime = 0;   //바로 움직이자
        isMove = false;
    }

    private void Update()
    {
        if (isMove)
        {
            timer += Time.deltaTime;
            float percentage = timer / duration;
            animationTransform.LookAt(targetPos);

            if (duration != 0)
            {
                animationTransform.position = Vector3.Lerp(startPos, targetPos, percentage);
            }

            if(animationTransform.position == targetPos)
            {
                isMove = false;
                waitTime = Random.Range(5, 20);
                timer = 0f;
                animationIdle();
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
            if(waitTime <= 0f)
            {
                targetPos = setRandomVector(gameObject.transform.position);
                startPos = gameObject.transform.position;
                isMove = true;
                animationGo();
            }
        }
    }
    // 가는 모션
    private void animationGo()
    {
        try
        {
            anim.Play("Walk");
        }
        catch
        {
            anim.Play("walk_01");
        }
    }
    // 멈추는 모션
    private void animationIdle()
    {
        try
        {
            anim.Play("Idle_A");
        }
        catch
        {
            anim.Play("idle_01");
        }
    }
    // 인사
    private void animationHi()
    {
        try
        {
            anim.Play("Bounce");
        }
        catch
        {
            anim.Play("win_dance_01");
        }
    }

    // 해당 모델이 가진 소리 재생
    public void playAudio()
    {
        gameObject.GetComponent<AudioSource>().loop = false;
        gameObject.GetComponent<AudioSource>().Play();
    }


    // 해당 모델을 터치했을 때
    public void touched()
    {
        if (isMove)
        {
            //멈춤
            targetPos = gameObject.transform.position;
            timer = duration;
        }
        //애니메이션
        animationHi();
        animationIdle();
    }

    // 랜덤한 3D위치 정하기 (but 높이 = y 는 그대로)
    Vector3 setRandomVector(Vector3 point)
    {
        var x = point.x + Random.Range(-5, 5);
        if(Mathf.Abs(x) > 10)
        {
            if (x > 0) x = 10;
            else x = -10;
        }
        var z = point.z + Random.Range(-5, 5);
        if (Mathf.Abs(z) > 10)
        {
            if (z > 0) z = 10;
            else z = -10;
        }
        duration = (Mathf.Abs(point.x - x) + Mathf.Abs(point.z - z))*2f;
        return new Vector3(x, point.y, z);
    }
}
