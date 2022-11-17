using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayRoomModelScript : MonoBehaviour
{
    private Animator anim;      //애니메이션
    private bool hasIntegerId;  //모델 타입 구분
    private bool isMove;        //움직이는 중인가?
    private float waitTime;         //움직이기까지 남은 시간
    private Vector3 targetPos;  //도착지점
    public Camera camera;
    private void Awake()
    {
        Debug.Log("Hello!");
        anim = gameObject.GetComponent<Animator>();
    }
    private void Start()
    {
        foreach (var param in anim.parameters)
        {
            if (param.Equals("actionID"))
            {
                hasIntegerId = true;
                break;
            }
        }
        animationIdle();
        waitTime = Random.Range(0, 10);
        isMove = false;
    }

    private void Update()
    {
        if (isMove)
        {
            animationGo();
            gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPos, 1f);

            if(gameObject.transform.position == targetPos)
            {
                isMove = false;
                waitTime = Random.Range(3, 10);
                animationIdle();
            }
        }
        else
        {
            waitTime -= 1f;
            if(waitTime <= 0f)
            {
                targetPos = setRandomVector(gameObject.transform.position);
                isMove = true;
            }
        }
    }
    // 가는 모션
    private void animationGo()
    {
        if (!hasIntegerId)
        {
            anim.Play("Walk");
        }
        else
        {
            anim.SetInteger("actionID", 2);
        }
    }
    // 멈추는 모션
    private void animationIdle()
    {
        if (!hasIntegerId)
        {
            anim.Play("Idle");
        }
        else
        {
            anim.SetInteger("actionID", 1);
        }
    }
    // 인사
    private void animationHi()
    {
        if (!hasIntegerId)
        {
            anim.Play("hello");
        }
        else
        {
            anim.SetInteger("actionID", 2);
        }
    }

    // 해당 모델이 가진 소리 재생
    public void playAudio()
    {
        gameObject.GetComponent<AudioSource>().loop = false;
        gameObject.GetComponent<AudioSource>().Play();
    }

    // 해당 모델의 지정한 애니메이션 재생
    public void playAnimation(string emotionName)
    {
        if (hasIntegerId)
        {
            anim.SetInteger("actionID", int.Parse(emotionName));
        }
        else
        {
            anim.Play(emotionName);
        }
    }

    // 해당 모델을 터치했을 때
    public void touched()
    {
        if (isMove)
        {
            //멈춤
            targetPos = gameObject.transform.position;
        }
        //카메라 봄
        gameObject.transform.LookAt(camera.transform);
        //애니메이션
        animationHi();
    }

    // 랜덤한 3D위치 정하기 (but 높이 = y 는 그대로)
    Vector3 setRandomVector(Vector3 point)
    {
        return new Vector3(point.x + Random.value * 100, point.y, point.z + Random.value * 100);
    }
}
