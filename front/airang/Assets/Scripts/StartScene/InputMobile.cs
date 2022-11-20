using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMobile : MonoBehaviour
{
    public Vector3 touchObj;

    private void Update()
    {
        // OnsSingleTouch();
        // 현재 터치되어 있는 카운트 가져오기
        int cnt = Input.touchCount;

        //		Debug.Log( "touch Cnt : " + cnt );

        // 동시에 여러곳을 터치 할 수 있기 때문.
        for (int i = 0; i < cnt; ++i)
        {
            // i 번째로 터치된 값 이라고 보면 된다. 
            Touch touch = Input.GetTouch(i);
            Vector3 pos = touch.position;

            Debug.Log("touch(" + i + ") : x = " + pos.x + ", y = " + pos.y + ",z = " + pos.z);
        }
    }
   /* private void OnsSingleTouch()
    {
        // 현재 화면을 터치한 손가락 개수가 1개이상일때
        if (Input.touchCount > 0)
        {
            // 현재 화면에 인식된 첫번째 손가락 터치 정보를 가져온다.
            Touch touch = Input.GetTouch(0);

            // 터치의 상태가 터치 시작일 때
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("터치가 시작됐따아아");
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Touch End");
            }
        }
    }*/

}
