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
        // ���࿡ �ε����� 0�϶� ����ī�޶� ��ġ���� ���ٸ�, canvas Active�� true�� ������.
        if (this.gameObject.transform.position == subCameras[0].transform.position)
        {
            // �̷��� Canvas�� setActive�� ����߸´°���.
            userBookListCanvas.SetActive(true);



        }
    }

}
