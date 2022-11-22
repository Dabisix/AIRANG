using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayRoomModelScript : MonoBehaviour
{
    private bool[] isMove;        //움직이는 중인가?
    private Animator[] anim;      //애니메이션
    private Transform[] animationTransform;   //현재 모델 타입

    private float[] waitTime;         //움직이기까지 남은 시간
    private float[] timer;
    private Vector3[] startPos;   //시작 지점
    private Vector3[] targetPos;  //도착지점
    private float[] duration; //몇초동안 움직일 것인가

    private void Awake()
    {
        anim = gameObject.GetComponentsInChildren<Animator>();
        animationTransform = new Transform[gameObject.transform.childCount];
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            animationTransform[i] = gameObject.transform.GetChild(i).gameObject.transform;
        }
    }
    private void Start()
    {
        int count = gameObject.transform.childCount;
        waitTime = new float[count];
        timer = new float[count];
        startPos = new Vector3[count];
        targetPos = new Vector3[count];
        duration = new float[count];
        isMove = new bool[count];
        for(int i = 0; i < count; i ++)
        {
            timer[i] = 1f;
        }
    }

    private void Update()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (isMove[i])
            {
                timer[i] += Time.deltaTime;
                float percentage = timer[i] / duration[i];
                animationTransform[i].LookAt(targetPos[i]);

                if (duration[i] != 0)
                {
                    animationTransform[i].position = Vector3.Lerp(startPos[i], targetPos[i], percentage);
                }

                if (animationTransform[i].position == targetPos[i])
                {
                    isMove[i] = false;
                    waitTime[i] = Random.Range(5, 20);
                    timer[i] = 0f;
                    anim[i].Play("Idle_A");
                }
            }
            else
            {
                waitTime[i] -= Time.deltaTime;
                if (waitTime[i] <= 0f)
                {
                    targetPos[i] = setRandomVector(animationTransform[i].position, i);
                    startPos[i] = animationTransform[i].position;
                    isMove[i] = true;
                    anim[i].Play("Walk");
                }
            }
        }
    }

    // 해당 모델이 가진 소리 재생
    public void playAudio()
    {
        gameObject.GetComponent<AudioSource>().loop = false;
        gameObject.GetComponent<AudioSource>().Play();
    }


    // 해당 모델을 터치했을 때
    public void touched(string name)
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (animationTransform[i].name.Equals(name))
            {
                if (isMove[i])
                {
                    //멈춤
                    targetPos[i] = animationTransform[i].position;
                    timer[i] = duration[i];
                }
                //애니메이션
                anim[i].Play("Bounce");
                anim[i].Play("Idle_A");
            }
        }
        
    }

    // 랜덤한 3D위치 정하기 (but 높이 = y 는 그대로)
    Vector3 setRandomVector(Vector3 point, int i)
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
        duration[i] = (Mathf.Abs(point.x - x) + Mathf.Abs(point.z - z))*2f;
        return new Vector3(x, point.y, z);
    }
}
