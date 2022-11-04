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
    [SerializeField] private Camera arCamera;
    Animator anim;
    bool isMove = false;
    Ray ray;
    RaycastHit hitobj;
    GameObject realTurtle;

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
        else if (TryGetTouchPosition(out Vector2 touchPosition))
        {

            if (Input.touchCount == 0) return;
            Touch touch = Input.GetTouch(0);
            //터치 시작시
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("===============================터치 시작했음");
                Debug.Log("거북이 오브젝트가 할당되었낭? : "+realTurtle);

                ray = Camera.main.ScreenPointToRay(touch.position);
                Physics.Raycast(ray, out hitobj);
                //Ray를 통한 오브젝트 인식
                Debug.Log("Ray를 통한 오브젝트 인식=============================");
                Debug.Log("지금 터치한 오브젝트 이름 뭐임? : " + hitobj.collider.name);

                //터치한 곳에 오브젝트 이름이 Turtle을 포함하면
                if (hitobj.collider.name.Contains("Turtle"))
                {
                    Debug.Log("거북이다!!!!!!!!!!!");
                    realTurtle = hitobj.collider.gameObject; //현재 충돌한거를 진짜 거북이에 넣었음
                    Debug.Log("거북이 넣었지롱 : " + realTurtle);
                    isMove = true;
                }
                else
                {
                    Debug.Log("엥?????????????????????????????????????????");
                }

            }
        }


        if (isMove && realTurtle != null)
        {
            Debug.Log("거북이가 움직이기 시작한다아아아앙");
            anim = realTurtle.GetComponent<Animator>(); //거북이 오브젝트

            Debug.Log("움직일거임??");
            Vector3 animationStartPos = realTurtle.transform.position;
            Vector3 animationTargetPos = animationStartPos+new Vector3(-5, 0, 0);

            float timer = 0f;
            float duration = 10f;
            timer += Time.deltaTime;
            float percentage = timer / duration;


            // 해당 애니메이션의 위치가 목표 위치로 이동했을때 멈추게
            if (realTurtle.transform.position == animationTargetPos)
            {
                anim.Play("Idle_A");    //기본 애니메이션으로 변경(점프하면서 멈추는 것 방지용)
            }
            // 움직이는 경우에만??
            else
            {
                anim.Play("Walk");
                realTurtle.transform.position = Vector3.Lerp(animationStartPos, animationTargetPos, percentage);
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

    //추가 : 터치 인식할 plane 확인 위하여
    TrackableId _trackableId;
    // 추가 : 터치된 지점 확인 위한 모양
    public GameObject m_touchedPrefab;

}