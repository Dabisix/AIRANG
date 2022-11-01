using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAnimation : MonoBehaviour
{
    public AnimationCurve turtleCurve;
    public Transform turtleTransform;

    Vector3 turtleStartPos; //시작 위치
    Vector3 turtleTargetPos; //목표 위치

    float timer = 0.0f;
    float duration = 15f; //지속 시간동안 이동시킴

    // Start is called before the first frame update
    void Start()
    {
        turtleStartPos = turtleTransform.position;
        turtleTargetPos = turtleStartPos + new Vector3(-5, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float percentage = timer / duration;
        turtleTransform.position = Vector3.Lerp(turtleStartPos, turtleTargetPos, turtleCurve.Evaluate(percentage));

        if (turtleTransform.position == turtleTargetPos)
        {
            Debug.Log("만났으니까 멈출거임");
            PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
}
