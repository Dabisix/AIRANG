using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(ARAnchorManager))]
[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class SwpAr : MonoBehaviour
{
    Animator TigerAnim;
    Animator RabbitAnim;
    Animator PeacockAnim;
    Animator DwarfHomeAnim;
    Animator PronghornAnim;
    Animator BirdAnim;
    Animator PandaAnim;
    //bool isMove = false;
    //bool isTime = false;
    Ray ray;
    RaycastHit hitobj;
    GameObject TigerObject;
    GameObject RabbitObject;
    GameObject BirdObject;
    GameObject PeacockObject;
    GameObject DwarfHome;
    GameObject PronghornObject;
    GameObject PandaObject;


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

                //터치한 곳에 오브젝트 이름이 DwarfHome을 포함하면
                if (hitobj.collider.name == "DwarfHome")
                {
                    Debug.Log("다음 페이지로 이동할 시간");
                    //BookManager.getInstance().CurPage += 1;
                    //BookManager.getInstance().changeScene();
                    DwarfHome = hitobj.collider.gameObject;
                    GameObject Door = DwarfHome.transform.Find("DoorPivot").gameObject;
                    DwarfHomeAnim = Door.GetComponent<Animator>();
                    DwarfHomeAnim.SetBool("isDoorOpen", true);
                    Door.GetComponent<AudioSource>().loop = false;
                    Door.GetComponent<AudioSource>().Play();
                    int numChild = m_RendedObject.transform.childCount;
                    for (int i = 0; i < numChild; i++)
                    {
                        if (m_RendedObject.transform.GetChild(i).name == "Canvas")
                        {
                            int canvasChiild = m_RendedObject.transform.GetChild(i).childCount;
                            for (int j = 0; j < canvasChiild; j++)
                            {
                                if (m_RendedObject.transform.GetChild(i).GetChild(j).name == "Guide")
                                {
                                    m_RendedObject.transform.GetChild(i).GetChild(j).gameObject.SetActive(false);
                                }
                                if (m_RendedObject.transform.GetChild(i).GetChild(j).name == "Result")
                                {
                                    m_RendedObject.transform.GetChild(i).GetChild(j).gameObject.SetActive(true);
                                    
                                    // 다음페이지로 넘어가자~
                                    break;
                                }

                            }
                           
                        }
                    }
                    

                }
                else if (hitobj.collider.name == "Tiger")
                {
                    // 호랑이를 선택했다면
                    TigerObject = hitobj.collider.gameObject; //터치한 오브젝트가 호랑이다
                    TigerAnim = TigerObject.GetComponent<Animator>(); // 호랑이 애니메이션
                    TigerAnim.Play("Jump");
                    TigerObject.GetComponent<AudioSource>().loop = false;
                    TigerObject.GetComponent<AudioSource>().Play();
                    Debug.Log("호랑이");
                }
                else if (hitobj.collider.name == "Peacock")
				{
                    // 공작새를 선택했다면
                    PeacockObject = hitobj.collider.gameObject; //터치한 오브젝트가 공작새다
					PeacockAnim = PeacockObject.GetComponent<Animator>(); //공작새 애니메이션
                    PeacockObject.GetComponent<AudioSource>().loop = false;
                    PeacockObject.GetComponent<AudioSource>().Play();
                    PeacockAnim.Play("Clicked");

                    Debug.Log("공작새");
                }
                else if (hitobj.collider.name == "Rabbit")
                {
                    // 공작새를 선택했다면
                    RabbitObject = hitobj.collider.gameObject; //터치한 오브젝트가 토끼다
                    RabbitAnim = RabbitObject.GetComponent<Animator>(); //토끼 애니메이션
                    RabbitObject.GetComponent<AudioSource>().loop = false;
                    RabbitObject.GetComponent<AudioSource>().Play();
                    RabbitAnim.Play("Jump");
                    Debug.Log("토끼");
                }
                else if (hitobj.collider.name == "Bird")
                {
                    BirdObject.GetComponent<AudioSource>().loop = false;
                    BirdObject.GetComponent<AudioSource>().Play();
                }
                else if (hitobj.collider.name == "Pronghorn")
                {
                    PronghornObject = hitobj.collider.gameObject;
                    PronghornAnim = PronghornObject.GetComponent<Animator>();
                    PronghornAnim.Play("Jump");

                    Debug.Log("사슴? 얘이름뭐지");
                }
                else if (hitobj.collider.name == "Panda")
                {
                    PandaObject = hitobj.collider.gameObject;
                    PandaAnim = PandaObject.GetComponent<Animator>();
                    PandaAnim.Play("Idle_B");

                    Debug.Log("사슴? 얘이름뭐지");
                }
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


    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    List<ARAnchor> m_AnchorPoints;
    public GameObject m_AnchorPrefab;
    GameObject m_RendedObject;

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
