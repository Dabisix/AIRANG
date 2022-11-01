using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ִϸ��̼� Ŀ�긦 ����� ���ӿ�����Ʈ �̵���Ű��
public class AnimationTest : MonoBehaviour
{
    public AnimationCurve turtleCurve;
    public Transform turtleTransform;

    public AnimationCurve rabbitCurve;
    public Transform rabbitTransform;

    Vector3 turtleStartPos; //���� ��ġ
    Vector3 turtleTargetPos; //��ǥ ��ġ
    Vector3 rabbitStartPos;
    Vector3 rabbitTargetPos;
    float timer = 0.0f;
    float duration = 10f; //���� �ð����� �̵���Ŵ

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
            Debug.Log("�������ϱ� �������");
            PauseGame();
        }
        Invoke("JumpRabbit", 0.5f);
    }

    private void JumpRabbit()
    {
        Debug.Log("�䳢 �����δ�");
        timer += Time.deltaTime;
        float percentage = timer / duration;
        rabbitTransform.position = Vector3.Lerp(rabbitStartPos, rabbitTargetPos, rabbitCurve.Evaluate(percentage));
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
}
