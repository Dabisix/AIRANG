using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARAnchorManager))]
[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class PlayRoomARScript : MonoBehaviour
{
    // 새로운 Prefab 가져오기
    GameObject loadPrefab(string prefabName)
    {
        return Resources.Load("Prefabs/PlayRoomScene/" + prefabName) as GameObject;
    }
    // 모든 Anchor를 삭제
    void RemoveAllAnchors()
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
    }
    private void Start()
    {
        //랜덤 세트 가져오기
        int setNumber = Random.Range(0, 2);
        m_LoadedPrefab = loadPrefab("Set"+setNumber);
    }
    void Update()
    {
        // Raycast 함수로 화면의 정중앙에서 레이져를 쏘고 해당 경로에 생성된 AR Plane이 있을 경우
        // 여기 코드에서는 렌더링된 Anchor가 1개 미만일 경우라는 조건도 추가함
        if (m_AnchorPoints.Count < 1 && m_RaycastManager.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), p_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = p_Hits[0].pose;
            /*
                AR로 생성된 여러 객체들을 Trackable이라고 한다
                ARRaycastManager의 Raycast는 자체적으로 Hit 정보에 어떤 Trackable이 맞았는지 알려준다
            */

            var hitTrackableId = p_Hits[0].trackableId;
            _trackableId = hitTrackableId;
            var hitPlane = m_PlaneManager.GetPlane(hitTrackableId);

            // Plane 정보를 가져오고 anchor를 생성, 그 Anchor위에 Prefab을 생성함
            var anchor = m_AnchorManager.AttachAnchor(hitPlane, hitPose);
            
            if (anchor == null)
            {
                Debug.Log("Error creating anchor.");
            }
            else
            {
                m_AnchorPoints.Add(anchor);
                createPrefab();
                TogglePlaneDetection();
            }
        }
    }

    // 로드한 prefab을 새로 create
    void createPrefab()
    {
        Transform originTransform = m_AnchorPoints[0].transform;
        float scale = Screen.height / 1200f;

        var created = Instantiate(m_LoadedPrefab, m_AnchorPoints[0].transform);
        created.AddComponent<PlayRoomModelScript>();
        m_RendedObject = created;
    }

    // 랜덤한 3D위치 정하기 (but 높이 = y 는 그대로)
    Vector3 setRandomVector(Vector3 point)
    {
        return new Vector3(point.x + Random.value * 100, point.y, point.z + Random.value * 100);
    }

    // 앵커 설정 후 Plane Detection을 비활성화
    void TogglePlaneDetection()
    {
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
    
    static List<ARRaycastHit> p_Hits = new List<ARRaycastHit>();
    List<ARAnchor> m_AnchorPoints;
    GameObject m_RendedObject;
    GameObject m_LoadedPrefab;
    ARRaycastManager m_RaycastManager;
    ARAnchorManager m_AnchorManager;
    ARPlaneManager m_PlaneManager;
    //추가 : 터치 인식할 plane 확인 위하여
    TrackableId _trackableId;
}
