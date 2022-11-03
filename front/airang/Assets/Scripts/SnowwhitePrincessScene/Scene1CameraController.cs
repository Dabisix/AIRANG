using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1CameraController : MonoBehaviour
{
    public Camera subCamera;
    public float speed;
    public iTween.EaseType easeType;

    public Camera targetCamera;

    public int page;

    void Start()
    {
        // 시작했을 때, 2초 있다가 백설공주쪽으로 이동하자.
        //Invoke("MoveCamera", 1.0f);
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 new_position = subCamera.transform.position;
        Vector3 new_rotation = subCamera.transform.eulerAngles;
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new_position, "easetype", easeType, "time", speed));
        iTween.RotateTo(this.gameObject, iTween.Hash("rotation", new_rotation, "easetype", easeType, "time", speed));
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position == subCamera.transform.position)
		{
            if(page == 7)
			{
                Invoke("transformCamera", 2.5f);
            }
		}
    }

    void transformCamera()
	{
        gameObject.transform.position = targetCamera.transform.position;
        gameObject.transform.rotation = targetCamera.transform.rotation;

    }
}

