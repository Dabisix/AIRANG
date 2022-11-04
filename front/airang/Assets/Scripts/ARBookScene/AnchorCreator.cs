using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Events;

[RequireComponent(typeof(ARAnchorManager))]
[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class AnchorCreator : MonoBehaviour
{
    
    public void loadContentPrefab()
    {
        RemoveAllAnchors();
        //m_AnchorPrefab = BookManager.getInstance().Content;
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
        //잘못 누른 앵커가 있으면
        if(failTouch != null && Time.time - _failTouchTime == 0.5f)
        {
            // 해당 초 지나면 삭제
            Destroy(failTouchObject);
            failTouch = null;
        }
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
            if (hitPlane.size.x * hitPlane.size.y > 2.5f)
            {
                // Plane 정보를 가져오고 anchor를 생성, 그 Anchor위에 Prefab을 생성함
                var anchor = m_AnchorManager.AttachAnchor(hitPlane, hitPose);

                // prefab 크기 변경
                anchor.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
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
        else if (TryGetTouchPosition(out Vector2 touchPosition) && m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            //터치 확인 & hit 한 경우
            //Queue 안 anchor 개수가 5개 이하면 넣는다
            if (m_touchAnchorList.Count < 5)
            {
                //터치 된 경우
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                Physics.Raycast(ray, out hit);

                //터치한 지점 위치
                var hitPose = s_Hits[0].pose;
                //해당 터치로 클릭된 부분의 trackableId 찾기
                var hitPlane = m_PlaneManager.GetPlane(_trackableId);
                var anchor = m_AnchorManager.AttachAnchor(hitPlane, hitPose);

                //어떤 것을 건드렸나 확인
                if (hit.collider.name.Equals("Obstacle"))
                {
                    //이름이 "Obstacle"인 장애물을 터치했을 경우
                    failTouch = anchor;
                    _failTouchTime = Time.time;
                    anchor.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                    failTouchObject = Instantiate(m_failTouchObject, anchor.transform);
                }
                else
                {
                    //만약 전 anchor이 있으면
                    if (m_touchAnchorList.Count > 0)
                    {
                        //전 anchor와 거리 비교 후 넣기
                        if (Vector3.Distance(anchor.transform.localPosition, m_touchAnchorList.Peek().transform.localPosition) > 0.7f)
                        {
                            m_touchAnchorList.Enqueue(anchor);
                            // 크기 조절
                            anchor.transform.localScale = new Vector3(2, 2, 2);
                            Vector3 tmp = anchor.transform.localPosition;
                            anchor.transform.localPosition = new Vector3(tmp.x, tmp.y, tmp.z + 0.05f);
                            m_touchRendedObject.Enqueue(Instantiate(m_touchedPrefab, anchor.transform));
                        }
                    }
                    else
                    {
                        m_touchAnchorList.Enqueue(anchor);
                        // 크기 조절
                        anchor.transform.localScale = new Vector3(2, 2, 2);
                        Vector3 tmp = anchor.transform.localPosition;
                        anchor.transform.localPosition = new Vector3(tmp.x, tmp.y, tmp.z + 0.05f);
                        m_touchRendedObject.Enqueue(Instantiate(m_touchedPrefab, anchor.transform));
                    }
                    if (hit.collider.name.Equals("EndObject") && m_endAnchor == null)
                    {
                        //이름이 "EndObjecct"인 오브젝트를 터치했을 경우
                        m_endAnchor = anchor;
                        Debug.Log("END");
                        endGame.Invoke();
                    }
                }
            }
        }
    }

    // 프리팹을 만들고 나면 Plane Detection을 비활성화
    void TogglePlaneDetection()
    {
        // m_PlaneManager.enabled = !m_PlaneManager.enabled;
        // 비활성화시 터치 부분을 찾을 수 없게 되니, 탐지 부분을 None로 변경
        if(m_PlaneManager.currentDetectionMode == PlaneDetectionMode.None)
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
    public GameObject m_AnchorPrefab;
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
    // 추가 : 터치 실패 지점 저장, 삭제 위하여
    ARAnchor failTouch;
    float _failTouchTime;
    GameObject failTouchObject;

    //추가 : 터치 인식할 plane 확인 위하여
    TrackableId _trackableId;
    // 추가 : 터치된 지점 확인 위한 모양
    public GameObject m_touchedPrefab;
    public GameObject m_failTouchObject;

    // 추가 : 종료 지점 터치
    public ARAnchor m_endAnchor;
    // 추가 : 종료 확인 위한 이벤트
    UnityEvent endGame;
}