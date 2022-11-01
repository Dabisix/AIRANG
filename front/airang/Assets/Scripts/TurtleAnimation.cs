using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAnimation : MonoBehaviour
{
    public AnimationCurve turtleCurve;
    public Transform turtleTransform;

    Vector3 turtleStartPos; //시작 위치
    Vector3 turtleTargetPos; //목표 위치

    public float timer;
    public float duration; //지속 시간동안 이동시킴
    public Vector3 vector;

    // Start is called before the first frame update
    void Start()
    {
        turtleStartPos = turtleTransform.position;
        turtleTargetPos = turtleStartPos + vector;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float percentage = timer / duration;
        turtleTransform.position = Vector3.Lerp(turtleStartPos, turtleTargetPos, turtleCurve.Evaluate(percentage));

        if (turtleTransform.position == turtleTargetPos)
        {
            Debug.Log("거북이가 멈춘당");
            Time.timeScale = 0;
        }
    }
}
