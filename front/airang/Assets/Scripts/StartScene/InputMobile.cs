using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMobile : MonoBehaviour
{
    public Vector3 touchObj;

    private void Update()
    {
        // OnsSingleTouch();
        // ���� ��ġ�Ǿ� �ִ� ī��Ʈ ��������
        int cnt = Input.touchCount;

        //		Debug.Log( "touch Cnt : " + cnt );

        // ���ÿ� �������� ��ġ �� �� �ֱ� ����.
        for (int i = 0; i < cnt; ++i)
        {
            // i ��°�� ��ġ�� �� �̶�� ���� �ȴ�. 
            Touch touch = Input.GetTouch(i);
            Vector3 pos = touch.position;

            Debug.Log("touch(" + i + ") : x = " + pos.x + ", y = " + pos.y + ",z = " + pos.z);
        }
    }
   /* private void OnsSingleTouch()
    {
        // ���� ȭ���� ��ġ�� �հ��� ������ 1���̻��϶�
        if (Input.touchCount > 0)
        {
            // ���� ȭ�鿡 �νĵ� ù��° �հ��� ��ġ ������ �����´�.
            Touch touch = Input.GetTouch(0);

            // ��ġ�� ���°� ��ġ ������ ��
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("��ġ�� ���۵Ƶ��ƾ�");
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Touch End");
            }
        }
    }*/

}
