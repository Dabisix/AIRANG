using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BackgroundMusicScript))]
public class TouchButtonAudioScript : MonoBehaviour
{
    AudioClip touchAudioClip;   //touch audio clip
    AudioSource audioSource; 
    private GraphicRaycaster gr;
    private BackgroundMusicScript script;

    private void Awake()
    {
        gr = GetComponent<GraphicRaycaster>();
        script = GetComponent<BackgroundMusicScript>();
        touchAudioClip = Resources.Load("Sounds/Button01") as AudioClip;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameManager.getInstance().GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (!script.toggle.isOn)
        {
            //터치했나 확인
            if (TryGetTouchPosition(out Vector2 touchPosition))
            {
                var ped = new PointerEventData(null);
                ped.position = touchPosition;
                List<RaycastResult> results = new List<RaycastResult>();
                gr.Raycast(ped, results);
                if (results.Count <= 0)
                {
                    return;
                }
                //터치했고, UI 위면
                foreach(var tmpItm in results)
                {
                    if (tmpItm.gameObject.GetComponent<Button>() != null || tmpItm.gameObject.GetComponent<Toggle>() != null)
                    {
                        audioSource.PlayOneShot(touchAudioClip);
                        break;
                    }
                }
            }
        }
    }
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

    // 추가 : 터치된 시간 간격 주기 위하여
    private float _time;
    // 추가 : 터치한 위치 확인 위하여
    private Vector2 touchPosition;
}
