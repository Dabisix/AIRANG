using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1CameraController : MonoBehaviour
{
    public float speed;
    public iTween.EaseType easeType;

    public Camera subCamera;
    public Camera targetCamera;
    public Camera[] subCameras;

    public int page;
    private int idx = 0;

    void Start()
    {
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
            if (page == 7)
            {
                Invoke("transformCamera", 2.5f);
            }
            if (page == 12)
            {
                subCamera = subCameras[idx + 1];
                MoveCamera();
            }
        }
    }

}
