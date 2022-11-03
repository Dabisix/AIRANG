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
        // 버튼 클릭 이벤트 지정
        Button btn = m_Button.GetComponent<Button>();
        btn.onClick.AddListener(InitAnchor);
    }

    void InitAnchor()
    {
        // AnchorCreator의 anchor 초기화 함수 실행
        m_ARSession_Origin.GetComponent<AnchorCreator>().RemoveAllAnchors();
    }
}
