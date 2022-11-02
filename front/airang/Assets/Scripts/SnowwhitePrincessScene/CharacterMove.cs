using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public AnimationCurve rabbitCurve;
    public Transform rabbitTransform;

    Vector3 rabbitStartPos;
    Vector3 rabbitTargetPos;

    public float timer;
    public float duration; //���� �ð����� �̵���Ŵ
    public Vector3 vector;

    public Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rabbitStartPos = rabbitTransform.position;
        rabbitTargetPos = rabbitStartPos + vector;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float percentage = timer / duration;
        rabbitTransform.position = Vector3.Lerp(rabbitStartPos, rabbitTargetPos, rabbitCurve.Evaluate(percentage));
        //�ִϸ����Ϳ� �� �ִϸ��̼��� ������Ѷ�...
        //Time.timeScale = 0;
        anim.SetBool("isStop", rabbitTransform.position == rabbitTargetPos);
    }

}
