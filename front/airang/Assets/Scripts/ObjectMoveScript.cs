using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectMoveScript : MonoBehaviour
{
    public AnimationCurve animationCurve;   //애니메이션 움직임 정도 (빠르게, 천천히)
    public Transform objectTransform;   //움직일 object
    public ARSessionOrigin arSessionOrigin; // anchor creator가 들어있는곳

    private Vector3 startPos; //시작 위치
    Vector3 targetPos; //목표 위치

    AnchorCreator m_anchorCreator;
    Queue<ARAnchor> m_touchAnchorList;
    Queue<GameObject> m_touchRendedObject;

    // Start is called before the first frame update
    float timer = 0.0f;
    public float duration = 4f; //지속 시간동안 이동시킴

    // Start is called before the first frame update
    void Start()
    {
        startPos = objectTransform.position;    //현재 위치
        targetPos = default;    //타겟 위치 초기화

        m_anchorCreator = FindObjectOfType<AnchorCreator>();
        //GetComponent<AnchorCreator>();    //앵커 크리에이터 가져오기
        m_touchAnchorList = m_anchorCreator.m_touchAnchorList;
        m_touchRendedObject = m_anchorCreator.m_touchRendedObject;
    }


    // Update is called once per frame
    void Update()
    {
        // 현재 타겟위치 없고, 찍은 좌표가 있을 경우에
        if (targetPos == default && m_touchAnchorList.Count > 0)
        {
            Animator animator = objectTransform.GetComponent<Animator>();
            animator.Play("Walk");
            targetPos = m_touchAnchorList.Dequeue().transform.localPosition;
            objectTransform.LookAt(targetPos);
        }
        else if(targetPos != default)
        {
            //움직이기 시작한 후
            timer += Time.deltaTime;
            float percentage = timer / duration;
            objectTransform.position = Vector3.Lerp(startPos, targetPos, animationCurve.Evaluate(percentage));
            if (objectTransform.position == targetPos)  //위치 도착하면
            {
                timer = 0.0f;
                Destroy(m_touchRendedObject.Dequeue());
                Animator animator = objectTransform.GetComponent<Animator>();
                animator.Play("Idle_A");    //기본 애니메이션으로 변경(점프하면서 멈추는 것 방지용)
                startPos = targetPos;   //현재 위치 변경
                targetPos = default;    //타겟 위치 재설정
            }
        }

        //완료 위치 왔을 때 
        if(m_anchorCreator.m_endAnchor != null && Vector3.Distance(objectTransform.position, m_anchorCreator.m_endAnchor.transform.localPosition) < 0.05f)
        {
            Debug.Log("포지션까지 확인!");
            m_anchorCreator.nextPage();
        }
    }
}
