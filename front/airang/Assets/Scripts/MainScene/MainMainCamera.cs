using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMainCamera : MonoBehaviour
{
    public GameObject userBookListCanvas;
    public Camera[] subCameras;
    public void MainMoveCamera(int index)
    {
        Vector3 new_position = subCameras[index].transform.position;
        Vector3 new_rotation = subCameras[index].transform.eulerAngles;
        //transform.position = new_position;
        //transform.eulerAngles = new_rotation;

        iTween.MoveTo(this.gameObject, iTween.Hash("position", new_position, "easetype", iTween.EaseType.easeOutQuint, "time", 5.0f));
        iTween.RotateTo(this.gameObject, iTween.Hash("rotation", new_rotation, "easetype", iTween.EaseType.easeOutQuint, "time", 5.0f));

    }
    private void Start()
    {
        MainMoveCamera(0);
    }

    private void Update()
    {
        // 만약에 인덱스가 0일때 서브카메라 위치까지 간다면, canvas Active를 true로 해주자.
        if (this.gameObject.transform.position == subCameras[0].transform.position)
        {
            // 이러면 Canvas를 setActive로 해줘야맞는거임.
            userBookListCanvas.SetActive(true);



        }
    }

}
