using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleTouch : MonoBehaviour
{
    public Transform turtleTransform;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("거북이  위치는 여기다 : " + turtleTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
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
                if(touchPos == turtleTransform.position)
                {
                    Debug.Log("거북이 터치했음");
                }
                else
                {
                    Debug.Log("누구냐 넌???");
                }
            }
        }
    }
}
