using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

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
    }

    void Awake()
    {
        // Awake 안에서 초기화 합시다!
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_AnchorManager = GetComponent<ARAnchorManager>();
        m_PlaneManager = GetComponent<ARPlaneManager>();
        m_AnchorPoints = new List<ARAnchor>();
        m_touchAnchorList = new();
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

            // Plane 정보를 가져오고 anchor를 생성, 그 Anchor위에 Prefab을 생성함
            var anchor = m_AnchorManager.AttachAnchor(hitPlane, hitPose);

            // prefab 크기 변경
            anchor.transform.localScale = new Vector3(1f, 1f, 1f);
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
        else
        {
            //터치 확인
            if (TryGetTouchPosition(out Vector2 touchPosition))
            {
                if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    //Queue 안 anchor 개수가 5개 이하면 넣는다
                    if (m_touchAnchorList.Count < 5)
                    {
                        //터치한 지점 위치
                        var hitPose = s_Hits[0].pose;
                        //해당 터치로 클릭된 부분의 trackableId 찾기
                        var hitPlane = m_PlaneManager.GetPlane(_trackableId);

                        var anchor = m_AnchorManager.AttachAnchor(hitPlane, hitPose);

                        //만약 전 anchor이 있으면
                        if (m_touchAnchorList.Count > 0)
                        {
                            //전 anchor와 거리 비교 후 넣기
                            if (Vector3.Distance(anchor.transform.localPosition, m_touchAnchorList.Peek().transform.localPosition) > 1f)
                            {
                                m_touchAnchorList.Enqueue(anchor);
                                // 크기 조절
                                anchor.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                                Instantiate(m_touchedPrefab, anchor.transform);
                            }
                        }
                        else
                        {
                            m_touchAnchorList.Enqueue(anchor);
                            // 크기 조절
                            anchor.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                            Instantiate(m_touchedPrefab, anchor.transform);
                        }
                    }
                }
            }
        }
    }

    // 프리팹을 만들고 나면 Plane Detection을 비활성화
    void TogglePlaneDetection()
    {
        // m_PlaneManager.enabled = !m_PlaneManager.enabled;
        // 비활성화시 터치 부분을 찾을 수 없으니 탐지 부분을 None로 변경
        m_PlaneManager.requestedDetectionMode = PlaneDetectionMode.None;
        // 원하는 Plane 제외하고 나머지를 사용 불가능하게
        foreach (ARPlane plane in m_PlaneManager.trackables)
        {
            if (plane.trackableId != _trackableId)
                plane.gameObject.SetActive(false);
        }
    }

    //터치 여부확인 (터치시 위치 저장)
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    List<ARAnchor> m_AnchorPoints;
    GameObject m_RendedObject;
    public GameObject m_AnchorPrefab;
    ARRaycastManager m_RaycastManager;
    ARAnchorManager m_AnchorManager;
    ARPlaneManager m_PlaneManager;

    // 추가 : 터치한 위치 확인 위하여
    Vector2 touchPosition;
    // 추가 : 터치된 위치 anchor 저장을 위하여
    Queue<ARAnchor> m_touchAnchorList;

    //추가 : 터치 인식할 plane 확인 위하여
    TrackableId _trackableId;
    // 추가 : 터치된 지점 확인 위한 모양
    public GameObject m_touchedPrefab;

}