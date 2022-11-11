using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    public CinemachineDollyCart obj;

    public DoorOpen doorOpen;
    public GameObject pathWay; // 다람쥐 길 오브젝트

    public float targetOperatingTime; // 예상 다람쥐 이동 시간 
    private float speed; // 다람쥐 속도
    private float length; // path의 길이
    private float currentTime; // 현재 경과 시간

    public GameObject changePath;
    public GameObject loginPath;
    public bool isStartScene;

    private bool changeScene = false;

	void Start()
    {

        // move to login canvas
        anim = gameObject.GetComponent<Animator>();
        obj = gameObject.GetComponent<CinemachineDollyCart>();
        // 다람쥐가 걸을 path의 길이 가져오기
        length = pathWay.GetComponent<CinemachinePath>().PathLength;
        speed = obj.m_Speed;
        currentTime = 0f;
		// 다람쥐 속도 가져오기
		// check Login
		if (isStartScene && PlayerPrefs.GetString("accessToken") != null)
		{
			obj.m_Path = loginPath.GetComponent<CinemachinePath>();
			length = obj.m_Path.PathLength;
			targetOperatingTime = (length - obj.m_Position) / speed;
			// 시간 - 거리 / 시간
			// get book list from server
			GameManager.getInstance().getAllBooksList();
			// move to Main
			Invoke("openDoor", 1.5f);
			changeScene = true;
		}
		else
		{
			targetOperatingTime = (length - obj.m_Position) / speed;
            // 시간 - 거리 / 시간
        }
       
    }

    // Update is called once per frame
    void Update()
    {

        if(!changeScene && currentTime >= targetOperatingTime)
        {
            anim.SetBool("isIdle", true);
            
		} else if (changeScene && currentTime >= targetOperatingTime)
		{
            SceneManager.LoadScene("MainScene");
        }
        else
		{
            currentTime += Time.deltaTime;
		}


    }


    public void ChangePath(int speedValue)
	{
        obj.m_Path = changePath.GetComponent<CinemachinePath>();
        obj.m_Speed = speedValue;
        obj.m_Position = 0;
        anim.SetBool("isIdle", false);
        anim.Play("Run");
        length = obj.m_Path.PathLength;
        // 다람쥐가 걸을 path의 길이 가져오기
        speed = obj.m_Speed;
        // 다람쥐 속도 가져오기
        targetOperatingTime = (length - obj.m_Position) / speed;
		if (isStartScene)
		{
            changeScene = true;
		}
        // 시간 - 거리 / 시간
        currentTime = 0f;
    }
    private void openDoor()
    {
        doorOpen.openDoor();
    }

}
