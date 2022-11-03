using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public bool isMove; //�̵� ����
    public AnimationCurve animationCurve;
    public Transform animationTransform;

    public string animationName; //�����ų �ִϸ��̼� �̸�

    Vector3 animationStartPos;
    Vector3 animationTargetPos;

    public float timer;
    public float duration; //���� �ð����� �̵���Ŵ
    public Vector3 vector;

    public Animator anim;


    private void Awake()
    {
        anim = animationTransform.GetComponent<Animator>();
    }

    void Start()
    {
        // ���� ������Ʈ�� ������ �Ÿ��� �ִٸ�
        if (isMove)
        {
            anim.SetBool("is" + animationName, true);
            animationStartPos = animationTransform.position;
            animationTargetPos = animationStartPos + vector;
        }
        else
        {
            anim.Play(animationName);
        }
    }

    void Update()
    {
        if (isMove)
        {
            timer += Time.deltaTime;
            float percentage = timer / duration;

            // �����̴� ��쿡��??
            if (duration != 0)
            {
                animationTransform.position = Vector3.Lerp(animationStartPos, animationTargetPos, animationCurve.Evaluate(percentage));
            }

            // �ش� �ִϸ��̼��� ��ġ�� ��ǥ ��ġ�� �̵������� ���߰�
            if (animationTransform.position == animationTargetPos)
            {
                Debug.Log("��ǥ ��ġ �����ߴ�");
                anim.SetBool("is"+animationName, false);
                //anim.speed = 0;
            }
        }
    }
}
