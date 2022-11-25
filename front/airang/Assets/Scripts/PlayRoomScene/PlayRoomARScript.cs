using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARCore;
using Unity.Collections;

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
        Destroy(m_RendedObject);
    }

    public void ResetArPlane()
    {
        foreach (var anchor in m_AnchorPoints)
            Destroy(anchor);
        m_AnchorPoints.Clear();
        Destroy(m_RendedObject);
        m_PlaneManager.requestedDetectionMode = PlaneDetectionMode.Horizontal;
    }

    void Awake()
    {
        // Awake 안에서 초기화 합시다!
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_AnchorManager = GetComponent<ARAnchorManager>();
        m_PlaneManager = GetComponent<ARPlaneManager>();
        m_AnchorPoints = new List<ARAnchor>();
        m_CameraManager = GetComponentInChildren<ARCameraManager>();
        m_FaceManager = GetComponent<ARFaceManager>();
        sessionOrigin = GetComponent<ARSessionOrigin>();
        isSelfCam = false;
        m_FaceManager.enabled = false;
    }
    private void Start()
    {
        //랜덤 세트 가져오기
        int setNumber = Random.Range(0, 2);
        m_LoadedPrefab = loadPrefab("Set"+setNumber);
    }
    void Update()
    {
        //셀프캠 아닐때
        if (!isSelfCam)
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
            else if (TryGetTouchPosition(out Vector2 touchPosition))
            {
                //터치 된 경우
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                Physics.Raycast(ray, out hit);

                //어떤 것을 건드렸나 확인
                if (hit.collider.name.Contains("Model"))
                {
                    m_RendedObject.GetComponent<PlayRoomModelScript>().touched(hit.collider.name);
                }
            }
        }
        else
        {
            //셀프캠인 경우
            if (isMaterial)
            {
                //Material 사용하는 경우
                setFaceMaterial();
            }
            else
            {
                //Prefab 사용하는 경우
                ARCoreFaceSubsystem subsystem = (ARCoreFaceSubsystem)m_FaceManager.subsystem;
                foreach (ARFace face in m_FaceManager.trackables)
                {
                    subsystem.GetRegionPoses(face.trackableId, Unity.Collections.Allocator.Persistent, ref faceRegions);

                    foreach (ARCoreFaceRegionData faceRegion in faceRegions)
                    {
                        ARCoreFaceRegion regionType = faceRegion.region;
                        if (regionType == ARCoreFaceRegion.NoseTip)
                        {
                            if (!noseObject)
                            {
                                noseObject = Instantiate(nosePrefab, sessionOrigin.trackablesParent);
                            }
                            noseObject.transform.localPosition = faceRegion.pose.position;
                            noseObject.transform.localRotation = faceRegion.pose.rotation;
                        }
                        else if (regionType == ARCoreFaceRegion.ForeheadLeft)
                        {
                            if (!leftHeadObject)
                            {
                                leftHeadObject = Instantiate(leftHeadPrefab, sessionOrigin.trackablesParent);
                            }
                            leftHeadObject.transform.localPosition = faceRegion.pose.position;
                            leftHeadObject.transform.localRotation = faceRegion.pose.rotation;
                        }
                        else if (regionType == ARCoreFaceRegion.ForeheadRight)
                        {
                            if (!rightHeadObject)
                            {
                                rightHeadObject = Instantiate(rightHeadPrefab, sessionOrigin.trackablesParent);
                            }
                            rightHeadObject.transform.localPosition = faceRegion.pose.position;
                            rightHeadObject.transform.localRotation = faceRegion.pose.rotation;
                        }

                    }
                }
            }
        }
    }

    void setFaceMaterial()
    {
        foreach (ARFace face in m_FaceManager.trackables)
        {
            face.GetComponent<MeshRenderer>().material = faceMaterial;
        }
    }

    // 로드한 prefab을 새로 create
    void createPrefab()
    {
        m_RendedObject = Instantiate(m_LoadedPrefab, m_AnchorPoints[0].transform);
        if (m_LoadedPrefab.name.Contains("Set"))
        {
            m_RendedObject.AddComponent(typeof(PlayRoomModelScript));
        }
        //Destroy(m_LoadedPrefab);
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

    public void getNewCharacter(string modelName)
    {
        RemoveAllAnchors();
        m_LoadedPrefab = loadPrefab(modelName);
        createPrefab();
    }

    public void changeCameraManager()
    {
        isSelfCam = !isSelfCam;
        ResetArPlane();
        RemoveAllAnchors();
        DeleteAllFace();
        setFaceMaterial();
        if (isSelfCam)
        {
            m_CameraManager.requestedFacingDirection = CameraFacingDirection.User;
            m_PlaneManager.enabled = false;
            m_RaycastManager.enabled = false;
            m_AnchorManager.enabled = false;
            m_FaceManager.enabled = true;
        }
        else
        {
            m_CameraManager.requestedFacingDirection = CameraFacingDirection.World;
            m_PlaneManager.enabled = true;
            m_RaycastManager.enabled = true;
            m_AnchorManager.enabled = true;
            m_FaceManager.enabled = false;
        }
    }

    public void setFace(string name)
    {
        DeleteAllFace();
        setFaceMaterial();
        isMaterial = false;
        if (name.StartsWith("M_"))
        {
            faceMaterial = loadMaterial(name);
            isMaterial = true;
        }
        else
        {
            nosePrefab = loadPrefab(name+"/Nose");
            leftHeadPrefab = loadPrefab(name + "/LeftHead");
            rightHeadPrefab = loadPrefab(name + "/RightHead");
        }
    }

    private void DeleteAllFace()
    {
        Destroy(noseObject);
        Destroy(leftHeadObject);
        Destroy(rightHeadObject);
        faceMaterial = loadMaterial("M_Face00");
    }

    public Material loadMaterial(string name)
    {
        return Resources.Load("Prefabs/PlayRoomScene/" + name) as Material;
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
    // 추가 : 터치한 위치 확인 위하여
    Vector2 touchPosition;

    //추가 : 카메라 변경시 사용하는 매니저 변경
    ARCameraManager m_CameraManager;
    ARFaceManager m_FaceManager;
    private bool isSelfCam;

    //추가 : 셀프캠 변경시 사용하는 오브젝트
    private bool isMaterial;
    private Material faceMaterial;
    NativeArray<ARCoreFaceRegionData> faceRegions;
    ARSessionOrigin sessionOrigin;
    GameObject noseObject;
    GameObject leftHeadObject;
    GameObject rightHeadObject;
    GameObject nosePrefab;
    GameObject leftHeadPrefab;
    GameObject rightHeadPrefab;
}
