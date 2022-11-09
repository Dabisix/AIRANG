using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class SwpPage21 : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    Animator turtleAnim;
    Animator rabbitAnim;
    bool isMove = false;
    bool isFinish = false;
    Ray ray;
    RaycastHit hitobj;
    GameObject jarObject;

    GameObject bottle1;
    GameObject bottle2;
    GameObject bottle3;
    GameObject apple;
    GameObject guideMsg;
    GameObject touchNumObject;

    TextMeshProUGUI tocuhNumText;

    Vector3 appleStart;
    Vector3 appleEnd;
    Vector3 bottle1Start;
    Vector3 bottle2Start;
    Vector3 bottle3Start;

    AudioSource audio;

    int touchNum = 0;
    float timer = 0f;

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
            // 오브젝트 찾기
            for(int i=0; i<m_RendedObject.transform.childCount; i++)
            {
                GameObject targetObject = m_RendedObject.transform.GetChild(i).gameObject;

                if(targetObject.name == "Jar")
                {
                    jarObject = targetObject;
                }
                else if(targetObject.name == "Bottle1")
                {
                    bottle1 = targetObject;
                    bottle1Start = bottle1.transform.position;
                    audio = bottle1.GetComponent<AudioSource>(); //어차피 효과음 다 똑같으니 한번만 가져오겠음 
                }
                else if (targetObject.name == "Bottle2")
                {
                    bottle2 = targetObject;
                    bottle2Start = bottle2.transform.position;
                }
                else if (targetObject.name == "Bottle3")
                {
                    bottle3 = targetObject;
                    bottle3Start = bottle3.transform.position;
                }
                else if(targetObject.name == "Apple")
                {
                    apple = targetObject;
                    appleStart = apple.transform.position;
                    appleEnd = appleStart + new Vector3(0, 0.3f, 0);
                }
                else if(targetObject.name == "Canvas")
                {
                    int idx = targetObject.transform.childCount;
                    for(int j=0; j<idx; j++)
                    {
                        GameObject childObject = targetObject.transform.GetChild(j).gameObject;
                        if(childObject.name == "TouchNum")
                        {
                            touchNumObject = childObject;
                            tocuhNumText = touchNumObject.GetComponent<TextMeshProUGUI>();
                        }else if(childObject.name == "GuideMsg")
                        {
                            guideMsg = childObject;
                        }

                    }
                }
            }

            if (Input.touchCount == 0) return;
            Touch touch = Input.GetTouch(0);
            //터치 시작시
            if (touch.phase == TouchPhase.Began)
            {
                //Ray를 통한 오브젝트 인식
                ray = Camera.main.ScreenPointToRay(touch.position);
                Physics.Raycast(ray, out hitobj); //터치한 부분을 hitobj로 뽑아내겠다

                //터치한 오브젝트가 항아리가 맞으면
                if (hitobj.collider.name.Contains("Jar"))
                {

                    Debug.Log("터치 개수 : " + touchNum);
                    touchNum++;
                    tocuhNumText.text = touchNum.ToString(); //화면에 숫자 갱신하기
                    if (touchNum < 4)
                    {
                        timer = 0f;
                        //Debug.Log("오디오??" + bottle1.GetComponent<AudioSource>().);
                        //bottle1.GetComponent<AudioSource>().loop = false;
                        //bottle1.GetComponent<AudioSource>().Play();
                        isMove = true;
                    }
                }
            }
        }
        if (isMove)
        {
            timer += Time.deltaTime;

            if (touchNum == 1)
            {
                Debug.Log("1번 병 움직일 차례");
                Debug.Log("병을 찍어보자 : " + bottle1);

                moveToJar(bottle1, bottle1Start, jarObject.transform.position + new Vector3(0,0.1f,0));
            }else if(touchNum == 2)
            {
                Debug.Log("2번 병 움직일 차례");
                Debug.Log("병을 찍어보자 : " + bottle2);
                moveToJar(bottle2, bottle2Start, jarObject.transform.position + new Vector3(0, 0.1f, 0));
            }
            else if(touchNum == 3)
            {
                Debug.Log("3번 병 움직일 차례");
                Debug.Log("병을 찍어보자 : " + bottle3);
                moveToJar(bottle3, bottle3Start, jarObject.transform.position + new Vector3(0, 0.1f, 0));

            }
        }

        if (isFinish)
        {
            timer += Time.deltaTime;
            Debug.Log("사과를 찍어보자 : " + apple);
            //항아리속으로 들어가면 없애기

            if (apple.transform.position == appleEnd)
            {
                Debug.Log("다음 페이지로 이동할 시간");
                //BookManager.getInstance().CurPage += 1;
                //BookManager.getInstance().changeScene();
                isFinish = false;
            }
            else
            {
                apple.transform.position = Vector3.Slerp(appleStart, appleEnd, timer / 1.5f);
            }
        }
    }

    void moveToJar(GameObject moveObject, Vector3 startPos, Vector3 finish)
    {
        //항아리속으로 들어가면 없애기
        if (moveObject.transform.position == finish)
        {
            moveObject.SetActive(false);
            isMove = false;
            if (moveObject == bottle3)
            {
                timer = 0f;
                isFinish = true;
                apple.GetComponent<AudioSource>().loop = false;
                apple.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            moveObject.SetActive(true);
            moveObject.transform.position = Vector3.Slerp(startPos, finish, timer / 1.5f);
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
