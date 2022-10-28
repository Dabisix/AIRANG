using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARAnchorManager))]
[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class AnchorCreator : MonoBehaviour
{
    // ������ game object Prefab
    [SerializeField]
    GameObject m_AnchorPrefab;

    // C# ������ getter, setter
    public GameObject AnchorPrefab
    {
        get => m_AnchorPrefab;
        set => m_AnchorPrefab = value;
    }

    // ��� Anchor�� ����
    public void RemoveAllAnchors()
    {
        // Anchor�� �ν��Ͻ�ȭ�� Object ��� ����
        foreach (var anchor in m_AnchorPoints)
            Destroy(anchor);
        foreach (var obj in m_RendedObjects)
            Destroy(obj);
        m_RendedObjects.Clear();
        m_AnchorPoints.Clear();

        // �ٽ� �÷��� ���ؼ� Ȱ��ȭ
        TogglePlaneDetection();
    }

    void Awake()
    {
        // Awake �ȿ��� �ʱ�ȭ �սô�!
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_AnchorManager = GetComponent<ARAnchorManager>();
        m_PlaneManager = GetComponent<ARPlaneManager>();
        m_AnchorPoints = new List<ARAnchor>();
        m_RendedObjects = new List<GameObject>();
    }

    void Update()
    {
        // Raycast �Լ��� ȭ���� ���߾ӿ��� �������� ��� �ش� ��ο� ������ AR Plane�� ���� ���
        // ���� �ڵ忡���� �������� Anchor�� 1�� �̸��� ����� ���ǵ� �߰���
        if (m_AnchorPoints.Count < 1 && m_RaycastManager.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = s_Hits[0].pose;
            /*
                AR�� ������ ���� ��ü���� Trackable�̶�� �Ѵ�
                ARRaycastManager�� Raycast�� ��ü������ Hit ������ � Trackable�� �¾Ҵ��� �˷��ش�
            */
            var hitTrackableId = s_Hits[0].trackableId;
            var hitPlane = m_PlaneManager.GetPlane(hitTrackableId);

            // Plane ������ �������� anchor�� ����, �� Anchor���� Prefab�� ������
            var anchor = m_AnchorManager.AttachAnchor(hitPlane, hitPose);
            var created = Instantiate(m_AnchorPrefab, anchor.transform);

            if (anchor == null)
            {
                Debug.Log("Error creating anchor.");
            }
            else
            {
                m_AnchorPoints.Add(anchor);
                m_RendedObjects.Add(created);
                TogglePlaneDetection();
            }
        }
    }

    // �������� ����� ���� Plane Detection�� ��Ȱ��ȭ
    void TogglePlaneDetection()
    {
        m_PlaneManager.enabled = !m_PlaneManager.enabled;

        foreach (ARPlane plane in m_PlaneManager.trackables)
        {
            plane.gameObject.SetActive(m_PlaneManager.enabled);
        }
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    List<ARAnchor> m_AnchorPoints;
    List<GameObject> m_RendedObjects;
    ARRaycastManager m_RaycastManager;
    ARAnchorManager m_AnchorManager;
    ARPlaneManager m_PlaneManager;
}