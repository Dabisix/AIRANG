using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;


public class MainMainCamera : MonoBehaviour
{
    public Camera[] subCameras;
    public GameObject bookListCanvas;
    public MainCharacterController mainCharacterController;
    public GameObject changePath;

    public GameObject[] paths;
    public void MainMoveCamera(int index)
    {
        if(gameObject.transform.position != subCameras[index].transform.position)
		{
            Vector3 new_position = subCameras[index].transform.position;
            Vector3 new_rotation = subCameras[index].transform.eulerAngles;
            //transform.position = new_position;
            //transform.eulerAngles = new_rotation;
            mainCharacterController.changePath = changePath;
            iTween.MoveTo(this.gameObject, iTween.Hash("position", new_position, "easetype", iTween.EaseType.easeOutQuint, "time", 5.0f));
            iTween.RotateTo(this.gameObject, iTween.Hash("rotation", new_rotation, "easetype", iTween.EaseType.easeOutQuint, "time", 5.0f));
            if (index == 2)
            {
                // if index is 2 ( selected camera is home camera), reverse path

                CinemachinePath path = mainCharacterController.obj.m_Path.GetComponent<CinemachinePath>();
                CinemachinePath new_path = mainCharacterController.changePath.GetComponent<CinemachinePath>();
                new_path.gameObject.transform.position = path.gameObject.transform.position;
                new_path.m_Waypoints.SetValue(path.m_Waypoints.GetValue(1), 0);
                new_path.m_Waypoints.SetValue(path.m_Waypoints.GetValue(0), 1);
                mainCharacterController.ChangePath(5);
            }
            else
            {
                mainCharacterController.changePath = paths[index];
                mainCharacterController.ChangePath(5);
            }
            if (index == 0)
            {
                bookListCanvas.SetActive(false);
            }
        }
        
    }
    private void Start()
    {
        // MainMoveCamera(0);
    }

    private void Update()
    {
        // 만약에 인덱스가 0일때 서브카메라 위치까지 간다면, canvas Active를 true로 해주자.
        if (this.gameObject.transform.position == subCameras[0].transform.position)
        {
            // 이떄뭘해주지?
        }
    }

}
