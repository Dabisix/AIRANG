using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMainCamera : MonoBehaviour
{
    public DoorOpen doorOpen;
    public Camera[] subCameras;
    public void StartMoveCamera(int index)
    {
        Vector3 new_position = subCameras[index].transform.position;
        Vector3 new_rotation = subCameras[index].transform.eulerAngles;
		//transform.position = new_position;
		//transform.eulerAngles = new_rotation;
        switch (index)
		{
            case 0:
                iTween.MoveTo(this.gameObject, iTween.Hash("position", new_position, "easetype", iTween.EaseType.easeInOutQuart, "time", 2.0f));
                iTween.RotateTo(this.gameObject, iTween.Hash("rotation", new_rotation, "easetype", iTween.EaseType.easeInOutQuart, "time", 2.0f));
                break;
            case 1:
                iTween.MoveTo(this.gameObject, iTween.Hash("position", new_position, "easetype", iTween.EaseType.easeInQuart, "time", 5.0f));
                iTween.RotateTo(this.gameObject, iTween.Hash("rotation", new_rotation, "easetype", iTween.EaseType.easeInQuart, "time", 5.0f));
                break;
        }
        

    }
    private void Start()
    {
        // check Login
        if (PlayerPrefs.GetString("accessToken") != "")
        {
            // get book list from server
            GameManager.getInstance().getAllBooksList();

            // move to Main

            Invoke("openDoor", 2.3f);
            StartMoveCamera(1);
        }
        else
        {
            // move to login canvas
            StartMoveCamera(0);
        }
    }

    private void openDoor()
    {
        doorOpen.openDoor();
    }


    private void Update()
    {
        // 만약에 인덱스가 1일때는 문을 열고 방으로 들어가고 씬을 변경해주는것이다.
        if (this.gameObject.transform.position == subCameras[1].transform.position)
        {
            // 씬이동해라.
            SceneManager.LoadScene("MainScene");
        }
    }
}
