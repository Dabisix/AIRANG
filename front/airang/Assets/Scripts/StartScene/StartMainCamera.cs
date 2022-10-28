using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMainCamera : MonoBehaviour
{
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
                iTween.MoveTo(this.gameObject, iTween.Hash("position", new_position, "easetype", iTween.EaseType.easeInOutQuart, "time", 5.0f));
                iTween.RotateTo(this.gameObject, iTween.Hash("rotation", new_rotation, "easetype", iTween.EaseType.easeInOutQuart, "time", 5.0f));
                break;
            case 1:
                iTween.MoveTo(this.gameObject, iTween.Hash("position", new_position, "easetype", iTween.EaseType.easeInQuart, "time", 5.0f));
                iTween.RotateTo(this.gameObject, iTween.Hash("rotation", new_rotation, "easetype", iTween.EaseType.easeInQuart, "time", 5.0f));
                break;
        }
        

    }
    private void Start()
    {
        StartMoveCamera(0);
    }


    private void Update()
    {
        // ���࿡ �ε����� 1�϶��� ���� ���� ������ ���� ���� �������ִ°��̴�.
        if (this.gameObject.transform.position == subCameras[1].transform.position)
        {
            // ���̵��ض�.
            SceneManager.LoadScene("MainScene");
        }
    }
}
