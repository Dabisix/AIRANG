using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TurtleTouch : MonoBehaviour
{
    public Transform turtleTransform;
    private ARRaycastManager raycastMgr;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    [SerializeField] private Camera arCamera;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("거북이  위치는 여기다 : " + Camera.main.ScreenToWorldPoint(turtleTransform.position));
    }

    // Update is called once per frame
    void Update()
    {
        /**
        // 화면 터치한 개수가 1개 이상일때
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); //첫번째 터치 정보 가져오기
            Vector3 touchPos;

            // 터치 시작했을때
            if(touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosToVector3 = new Vector3(touch.position.x, touch.position.y, 100);
                touchPos = Camera.main.ScreenToWorldPoint(touchPosToVector3);
                if(touchPos == Camera.main.ScreenToWorldPoint(turtleTransform.position))
                {
                    Debug.Log("거북이 터치했음");
                }
                else
                {
                    Debug.Log("누구냐 넌???");
                }
            }
        }
        **/

        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);



        //터치 시작시
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray;
            RaycastHit hitobj;

            ray = arCamera.ScreenPointToRay(touch.position);

            //Ray를 통한 오브젝트 인식
            if (Physics.Raycast(ray, out hitobj))

            {

                //터치한 곳에 오브젝트 이름이 Turtle을 포함하면

                if (hitobj.collider.name.Contains("Turtle"))
                {
                    Debug.Log("거북이다!!!!!!!!!!!");
                }
                else
                {

                    Debug.Log("엥?????????????????????????????????????????");
                }
            }
        }
    }
}
