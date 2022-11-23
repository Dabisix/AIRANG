using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class AR_1 : MonoBehaviour
{
    public SetBeforeAR setBeforeAR;
    Animator turtleAnim;
    Animator rabbitAnim;
    bool isMove = false;
    bool isTime = false;
    Ray ray;
    RaycastHit hitobj;
    GameObject turtleObject;
    GameObject rabbitObject;

    Vector3 turtleStartPos;
    Vector3 turtleTargetPos;
    Vector3 rabbitStartPos;
    Vector3 rabbitTargetPos;

    //카운트다운 오브젝트
    GameObject guideMsg;
    GameObject num_3;
    GameObject num_2;
    GameObject num_1;
    GameObject num_GO;
    private int countdown;
    AudioSource audioSource;
    AudioClip raceAudioClip;

    float timer = 0f;

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
        countdown = 0;
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
                setBeforeAR.GetPlane();
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
                    audioSource = m_RendedObject.GetComponent<AudioSource>();
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

                //터치한 곳에 오브젝트 이름이 Turtle을 포함하면
                if (hitobj.collider.name.Contains("Turtle"))
                {
                    turtleObject = hitobj.collider.gameObject; //터치한 오브젝트가 거북이다
                    turtleStartPos = turtleObject.transform.position; //거북이 시작위치
                    turtleTargetPos = turtleStartPos + new Vector3(-2, 0, 0); //거북이 도착위치
                    turtleAnim = turtleObject.GetComponent<Animator>(); //거북이 애니메이션

                    // 거북이 터치하면 경주 시작함, 토끼도 같이 움직여야 하기 때문에 토끼를 찾아보자
                    int numChild = m_RendedObject.transform.childCount;
                    for (int i = 0; i < numChild; i++)
                    {
                        if (m_RendedObject.transform.GetChild(i).name == "Rabbit")
                        {
                            rabbitObject = m_RendedObject.transform.GetChild(i).gameObject;
                        }
                        if (m_RendedObject.transform.GetChild(i).name == "Canvas") //카운트다운 이미지 오브젝트 찾기
                        {
                            int canvasChiild = m_RendedObject.transform.GetChild(i).childCount;
                            for (int j = 0; j < canvasChiild; j++)
                            {
                                GameObject targetObject = m_RendedObject.transform.GetChild(i).GetChild(j).gameObject;
                                //이름에 맞는 오브젝트 찾아서 넣기......... 
                                if (targetObject.name == "1")
                                {
                                    num_1 = m_RendedObject.transform.GetChild(i).GetChild(j).gameObject;
                                    continue;
                                }
                                if (targetObject.name == "2")
                                {
                                    num_2 = m_RendedObject.transform.GetChild(i).GetChild(j).gameObject;
                                    continue;
                                }
                                if (targetObject.name == "3")
                                {
                                    num_3 = m_RendedObject.transform.GetChild(i).GetChild(j).gameObject;
                                    continue;
                                }
                                if (targetObject.name == "GO")
                                {
                                    num_GO = m_RendedObject.transform.GetChild(i).GetChild(j).gameObject;
                                    continue;
                                }
                                if (targetObject.name == "Guide")
                                {
                                    guideMsg = m_RendedObject.transform.GetChild(i).GetChild(j).gameObject;
                                    continue;
                                }
                            }
                        }
                    }

                    rabbitStartPos = rabbitObject.transform.position; //토끼 시작위치
                    rabbitTargetPos = rabbitStartPos + new Vector3(-2, 0, 0); //토끼 도착위치
                    rabbitAnim = rabbitObject.GetComponent<Animator>(); //토끼 애니메이션

                    turtleAnim.SetBool("isWalk", true); //거북이는 걸어
                    rabbitAnim.SetBool("isJump", true); //토끼는 뛰어
                    audioSource.loop = false; //반복안함
                    audioSource.Play(); //카운트 다운 소리 재생
                    isTime = true; //카운트 다운 이미지 나오게
                }
            }
        }

        //카운트다운
        if (isTime)
        {
            if (countdown == 0)
            {
                Time.timeScale = 0;
            }

            if (countdown <= 90)
            {
                guideMsg.SetActive(false); //거북이 터치 메세지 false
                countdown++;

                if (countdown < 30)
                {

                    num_3.SetActive(true);
                }
                if (countdown > 30)
                {
                    num_3.SetActive(false);
                    num_2.SetActive(true);
                }
                if (countdown > 60)
                {
                    num_2.SetActive(false);
                    num_1.SetActive(true);
                }
                if (countdown >= 90)
                {
                    num_1.SetActive(false);
                    num_GO.SetActive(true);
                    StartCoroutine(LoadingEnd());
                    Time.timeScale = 1.0f; //게임 시작
                    isMove = true; //애니메이션 시작 flag
                    isTime = false;
                }
            }
        }

        IEnumerator LoadingEnd()
        {
            yield return new WaitForSeconds(1.0f);
            num_GO.SetActive(false);
        }

        if (isMove) //거북이 터치하면 애니메이션 시작
        {
            timer += Time.deltaTime;
            turtleObject.transform.position = Vector3.Lerp(turtleStartPos, turtleTargetPos, timer / 20f);
            rabbitObject.transform.position = Vector3.Lerp(rabbitStartPos, rabbitTargetPos, timer / 5f);


            // 5초 지나면 다음 페이지로 이동시키자
            if (timer >= 5f)
            {
                turtleAnim.SetBool("isWalk", false);
                rabbitAnim.SetBool("isJump", false);
                Debug.Log("다음 페이지로 이동할 시간");
                BookManager.getInstance().CurPage += 1;
                BookManager.getInstance().changeScene();
                isMove = false; //애니메이션 중지
            }
        }
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
}
