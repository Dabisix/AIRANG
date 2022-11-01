using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAnimation : MonoBehaviour
{
    public AnimationCurve turtleCurve;
    public Transform turtleTransform;

    Vector3 turtleStartPos; //���� ��ġ
    Vector3 turtleTargetPos; //��ǥ ��ġ

    float timer = 0.0f;
    float duration = 15f; //���� �ð����� �̵���Ŵ

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
            Debug.Log("�������ϱ� �������");
            PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
}
