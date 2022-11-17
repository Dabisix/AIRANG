using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRoomModelScript : MonoBehaviour
{
    public string emotionName;
    private Animator anim;
    private bool hasIntegerId;
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    private void Start()
    {
        foreach (var param in anim.parameters)
        {
            if (param.Equals("actionID"))
            {
                hasIntegerId = true;
                emotionName = "1";
                break;
            }
        }
        if (!hasIntegerId)
        {
            emotionName = "Idle";
        }
    }
   
    // 해당 모델이 가진 소리 재생
    public void playAudio()
    {
        gameObject.GetComponent<AudioSource>().loop = false;
        gameObject.GetComponent<AudioSource>().Play();
    }

    // 해당 모델의 지정한 애니메이션 재생
    public void playAnimation()
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

    //해당 모델을 지정한 곳으로 이동
    public void Move(Vector3 destination)
    {
        
    }

    // 랜덤한 3D위치 정하기 (but 높이 = y 는 그대로)
    Vector3 setRandomVector(Vector3 point)
    {
        return new Vector3(point.x + Random.value * 100, point.y, point.z + Random.value * 100);
    }
}
