using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    public AnimationCurve turtleCurve;
    public Transform turtleTransform;

    public AnimationCurve rabbitCurve;
    public Transform rabbitTransform;

    Vector3 turtleStartPos;
    Vector3 turtleTargetPos;
    Vector3 rabbitStartPos;
    Vector3 rabbitTargetPos;
    float timer = 0.0f;
    float duration = 10f;

    // Start is called before the first frame update
    void Start()
    {
        turtleStartPos = turtleTransform.position;
        turtleTargetPos = turtleStartPos + new Vector3(-4, 0.5f, 0);

        rabbitStartPos = rabbitTransform.position;
        rabbitTargetPos = rabbitStartPos + new Vector3(7.5f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float percentage = timer / duration;
        turtleTransform.position = Vector3.Lerp(turtleStartPos, turtleTargetPos, percentage);
        if(rabbitTransform.position == rabbitTargetPos)
        {

            Debug.Log("∏∏≥µ¿∏¥œ±Ó ∏ÿ√‚∞≈¿”");
            PauseGame();
        }
        Invoke("JumpRabbit", 0.5f);
    }

    private void JumpRabbit()
    {
        Debug.Log("≈‰≥¢ øÚ¡˜¿Œ¥Ÿ");
        timer += Time.deltaTime;
        float percentage = timer / duration;
        rabbitTransform.position = Vector3.Lerp(rabbitStartPos, rabbitTargetPos, percentage);

    }

    void PauseGame()
    {
        Time.timeScale = 0.1f;
        Time.timeScale = 0;
    }
}
