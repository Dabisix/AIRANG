using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class AR_3 : MonoBehaviour
{
    bool isMove = false;
    Ray ray;
    RaycastHit hitobj;
    GameObject queenObject;
    Animator queenAnim;

    float timer = 0f;
    Vector3 queenStartPos;
    Vector3 queenTargetPos;

    public void loadContentPrefab()
    {
        RemoveAllAnchors();
        m_AnchorPrefab = BookManager.getInstance().Content;
    }

    // 모든 Anchor를 삭제
    public void RemoveAllAnchors()
    {
        // Anchor와 인스턴스화된 Object 모두 삭제
        foreach (var anchor in m_AnchorPoints)
            Destroy(anchor);
        m_AnchorPoints.Clear();

        Destroy(m_RendedObject);

        //만약 터치가 있으면 터치의 anchor와 object 삭제
        foreach (var anchor in m_touchAnchorList)
            Destroy(anchor);
        m_touchAnchorList.Clear();
        foreach (var rendedObject in m_touchRendedObject)
            Destroy(rendedObject);
        m_touchRendedObject.Clear();
    }

    void Awake()
    {
        // Awake 안에서 초기화 합시다!
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_AnchorManager = GetComponent<ARAnchorManager>();
        m_PlaneManager = GetComponent<ARPlaneManager>();
        m_AnchorPoints = new List<ARAnchor>();

        //터치한 앵커의 리스트
        m_touchAnchorList = new();
        m_touchRendedObject = new();
        //페이지 들어온 시간
        _time = Time.time;
    }

    private void Start()
    {
        loadContentPrefab();
    }

    void Update()
    {
        // Raycast 함수로 화면의 정중앙에서 레이져를 쏘고 해당 경로에 생성된 AR Plane이 있을 경우
        // 여기 코드에서는 렌더링된 Anchor가 1개 미만일 경우라는 조건도 추가함
        if (m_AnchorPoints.Count < 1 && m_RaycastManager.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = s_Hits[0].pose;
            /*
                AR로 생성된 여러 객체들을 Trackable이라고 한다
                ARRaycastManager의 Raycast는 자체적으로 Hit 정보에 어떤 Trackable이 맞았는지 알려준다
            */
            var hitTrackableId = s_Hits[0].trackableId;
            _trackableId = hitTrackableId;
            var hitPlane = m_PlaneManager.GetPlane(hitTrackableId);


            // plane 크기에 따라 prefab 크기 설정 f이상이면 저장 (최소크기)
            Debug.Log("hitPlane.size.x * hitPlane.size.y" + (hitPlane.size.x * hitPlane.size.y));
            if (hitPlane.size.x * hitPlane.size.y > 3f)
            {
                // Plane 정보를 가져오고 anchor를 생성, 그 Anchor위에 Prefab을 생성함
                var anchor = m_AnchorManager.AttachAnchor(hitPlane, hitPose);

                // prefab 크기 변경
                anchor.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                var created = Instantiate(m_AnchorPrefab, anchor.transform);

                if (anchor == null)
                {
                    Debug.Log("Error creating anchor.");
                }
                else
                {
                    m_AnchorPoints.Add(anchor);
                    m_RendedObject = created;
                    TogglePlaneDetection();
                }
            }
        }
        else if (TryGetTouchPosition(out Vector2 touchPosition))
        {

            if (Input.touchCount == 0) return;
            Touch touch = Input.GetTouch(0);
            //터치 시작시
            if (touch.phase == TouchPhase.Began)
            {
                //Ray를 통한 오브젝트 인식
                ray = Camera.main.ScreenPointToRay(touch.position);
                Physics.Raycast(ray, out hitobj); //터치한 부분을 hitobj로 뽑아내겠다

                //터치한 곳에 오브젝트 이름이 Mirror를 포함하면
                if (hitobj.collider.name.Contains("Mirror"))
                {
                    Debug.Log("지금터치한애 : " + hitobj.collider.name);
                    int numIdx = m_RendedObject.transform.childCount;
                    for (int i = 0; i < numIdx; i++)
                    {
                        GameObject childObject = m_RendedObject.transform.GetChild(i).gameObject;
                        Debug.Log(childObject.name);
                        if (childObject.name == "Canvas")
                        {
                            childObject.SetActive(false); //가이드 메세지 없애기
                            continue;
                        }
                        if (childObject.name == "Queen")
                        {
                            queenObject = childObject;
                            queenAnim = queenObject.GetComponent<Animator>();
                        }
                    }

                    queenStartPos = queenObject.transform.position;
                    queenTargetPos = queenStartPos + new Vector3(-2, 0, 0);
                    queenAnim.SetBool("isWalk", true);
                    isMove = true; //카메라 이동 시작해라
                }
            }
        }

        if (isMove)
        {
            timer += Time.deltaTime;
            queenObject.transform.position = Vector3.Lerp(queenStartPos, queenTargetPos, timer / 10f);

            // 5초 지나면 다음 페이지로 이동시키자
            if (timer >= 5f)
            {
                isMove = false; //애니메이션 중지
                queenAnim.SetBool("isWalk", false);
                //Debug.Log("다음 페이지로 이동할 시간");
                BookManager.getInstance().CurPage += 1;
                BookManager.getInstance().changeScene();
            }
        }
    }

    void nextPage()
    {
        //Debug.Log("다음 페이지로 이동할 시간");
        BookManager.getInstance().CurPage += 1;
        BookManager.getInstance().changeScene();
    }

    // 프리팹을 만들고 나면 Plane Detection을 비활성화
    void TogglePlaneDetection()
    {
        // m_PlaneManager.enabled = !m_PlaneManager.enabled;
        // 비활성화시 터치 부분을 찾을 수 없게 되니, 탐지 부분을 None로 변경
        if (m_PlaneManager.currentDetectionMode == PlaneDetectionMode.None)
        {
            //넓은 바닥 인식
            m_PlaneManager.requestedDetectionMode = PlaneDetectionMode.Horizontal;
        }
        else
        {
            m_PlaneManager.requestedDetectionMode = PlaneDetectionMode.None;
            // 원하는 Plane 제외하고 나머지를 사용 불가능하게
            foreach (ARPlane plane in m_PlaneManager.trackables)
            {
                if (plane.trackableId != _trackableId)
                    plane.gameObject.SetActive(false);
            }
        }
    }

    //터치 여부확인 (터치시 위치 저장)
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0 && Time.time - _time >= 0.5f)
        {
            touchPosition = Input.GetTouch(0).position;
            _time = Time.time;
            return true;
        }
        touchPosition = default;
        return false;
    }



    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    List<ARAnchor> m_AnchorPoints;
    GameObject m_RendedObject;
    GameObject m_AnchorPrefab;
    ARRaycastManager m_RaycastManager;
    ARAnchorManager m_AnchorManager;
    ARPlaneManager m_PlaneManager;

    // 추가 : 터치한 위치 확인 위하여
    Vector2 touchPosition;
    // 추가 : 터치된 위치 anchor 저장을 위하여
    public Queue<ARAnchor> m_touchAnchorList;
    // 추가 : 터치된 시간 간격 주기 위하여
    float _time;
    // 추가 : 터치 인식 지점 삭제 위해서
    public Queue<GameObject> m_touchRendedObject;

    //추가 : 터치 인식할 plane 확인 위하여
    TrackableId _trackableId;
    // 추가 : 터치된 지점 확인 위한 모양
    GameObject m_touchedPrefab;
}
