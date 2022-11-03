using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.XR.ARFoundation;

public class InitializeAnchor : MonoBehaviour
{
    public Button m_Button;
    public ARSessionOrigin m_ARSession_Origin;

    void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ ����
        Button btn = m_Button.GetComponent<Button>();
        btn.onClick.AddListener(InitAnchor);
    }

    void InitAnchor()
    {
        // AnchorCreator�� anchor �ʱ�ȭ �Լ� ����
        m_ARSession_Origin.GetComponent<AnchorCreator>().RemoveAllAnchors();
    }
}
