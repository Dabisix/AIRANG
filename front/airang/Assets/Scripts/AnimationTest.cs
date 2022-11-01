using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 애니메이션 커브를 사용해 게임오브젝트 이동시키기
public class AnimationTest : MonoBehaviour
{
    public AnimationCurve turtleCurve;
    public Transform turtleTransform;

    public AnimationCurve rabbitCurve;
    public Transform rabbitTransform;

    Vector3 turtleStartPos; //시작 위치
    Vector3 turtleTargetPos; //목표 위치
    Vector3 rabbitStartPos;
    Vector3 rabbitTargetPos;
    float timer = 0.0f;
    float duration = 10f; //지속 시간동안 이동시킴

    // Start is called before the first frame update
    void Start()
    {
        turtleStartPos = turtleTransform.position;
        turtleTargetPos = turtleStartPos + new Vector3(-4, 0.5f, 0);

        rabbitStartPos = rabbitTransform.position;
        rabbitTargetPos = rabbitStartPos + new Vector3(6.8f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float percentage = timer / duration;
        turtleTransform.position = Vector3.Lerp(turtleStartPos, turtleTargetPos, turtleCurve.Evaluate(percentage));
        if(rabbitTransform.position == rabbitTargetPos)
        {
            Debug.Log("만났으니까 멈출거임");
            PauseGame();
        }
        Invoke("JumpRabbit", 0.5f);
    }

    private void JumpRabbit()
    {
        Debug.Log("토끼 움직인다");
        timer += Time.deltaTime;
        float percentage = timer / duration;
        rabbitTransform.position = Vector3.Lerp(rabbitStartPos, rabbitTargetPos, rabbitCurve.Evaluate(percentage));
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
}
