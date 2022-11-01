using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAnimation : MonoBehaviour
{
    public AnimationCurve turtleCurve;
    public Transform turtleTransform;

    Vector3 turtleStartPos; //���� ��ġ
    Vector3 turtleTargetPos; //��ǥ ��ġ

    public float timer;
    public float duration; //���� �ð����� �̵���Ŵ
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
            Debug.Log("�ź��̰� �����");
            Time.timeScale = 0;
        }
    }
}
