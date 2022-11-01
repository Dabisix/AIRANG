using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAnimation : MonoBehaviour
{
    public AnimationCurve rabbitCurve;
    public Transform rabbitTransform;

    Vector3 rabbitStartPos;
    Vector3 rabbitTargetPos;

    float timer = 0.0f;
    float duration = 15f; //지속 시간동안 이동시킴

    // Start is called before the first frame update
    void Start()
    {
        rabbitStartPos = rabbitTransform.position;
        rabbitTargetPos = rabbitStartPos + new Vector3(7.4f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float percentage = timer / duration;
        rabbitTransform.position = Vector3.Lerp(rabbitStartPos, rabbitTargetPos, rabbitCurve.Evaluate(percentage));
        if (rabbitTransform.position == rabbitTargetPos)
        {
            Time.timeScale = 0;
        }
    }
}
