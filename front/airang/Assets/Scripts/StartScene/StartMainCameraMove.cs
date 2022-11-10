using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMainCameraMove : MonoBehaviour
{
    Transform playerTransform; // 위치, 회전, 크기 컴포넌트
    Vector3 offset;
    public GameObject character;

    // 변수 초기화
    void Awake()
    {
        // FindGameObjectWithTag : 게임오브젝트에서 "Player"라는 태그를 가진 오브젝트 검색
        // 근데 find는 부하가 있어 잘 사용안한다고 함 그냥 public 변수로 선언한 뒤 갖다 넣는식으로 하는걸 권장
        playerTransform = character.transform;
        // 현재 위치에서 player 오브젝트의 위치를 빼줘 적당한 거리둠
        offset = transform.position - playerTransform.position;
    }

    // UI나 카메라 이동 관련 : Update에서 연산을 다 한 뒤에 따라붙는것들
    void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
    }
}
