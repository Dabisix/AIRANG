using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera subCameras;

    public void StartMoveCamera()
    {
        Vector3 new_position = subCameras.transform.position;
        Vector3 new_rotation = subCameras.transform.eulerAngles;
        //transform.position = new_position;
        //transform.eulerAngles = new_rotation;

        iTween.MoveTo(this.gameObject, iTween.Hash("position", new_position, "easetype", iTween.EaseType.easeOutSine, "time", 2.0f));
        iTween.RotateTo(this.gameObject, iTween.Hash("rotation", new_rotation, "easetype", iTween.EaseType.easeOutSine, "time", 2.0f));
    }

    // Start is called before the first frame update
    void Start()
    {
        StartMoveCamera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
