using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    CinemachineDollyCart obj;

    public GameObject pathWay; // 다람쥐 길 오브젝트

    public float targetOperatingTime; // 예상 다람쥐 이동 시간 
    private float speed; // 다람쥐 속도
    private float length; // path의 길이
    private float currentTime; // 현재 경과 시간

    public GameObject changePath;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        obj = gameObject.GetComponent<CinemachineDollyCart>();
        length = pathWay.GetComponent<CinemachinePath>().PathLength;
        // 다람쥐가 걸을 path의 길이 가져오기
        speed = obj.m_Speed;
        // 다람쥐 속도 가져오기
        targetOperatingTime = (length - obj.m_Position) / speed;
        // 시간 - 거리 / 시간
        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        if(currentTime >= targetOperatingTime)
        {
            anim.SetBool("isIdle", true);
            Debug.Log("도착했어!");

		}
		else
		{
            currentTime += Time.deltaTime;
		}


    }


    public void ChangePath()
	{
        obj.m_Path = changePath.GetComponent<CinemachinePath>();
        obj.m_Speed = 3;
        obj.m_Position = 0;
        anim.SetBool("isIdle", false);
        anim.Play("Run");
	}
}
