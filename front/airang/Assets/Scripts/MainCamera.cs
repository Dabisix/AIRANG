using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCamera : MonoBehaviour
{
    public Camera[] subCameras;
    // ���⿡ �� ������Ʈ�� ������ �س���, ���� ����ī�޶�1�γ��� move�� ���۵Ǹ� �׶� door�� ���°� �����Ͱ���.
    public void MoveCamera(int index)
    {
        Vector3 new_position = subCameras[index].transform.position;
        Vector3 new_rotation = subCameras[index].transform.eulerAngles;
        //transform.position = new_position;
        //transform.eulerAngles = new_rotation;

        iTween.MoveTo(this.gameObject, iTween.Hash("position", new_position, "easetype", iTween.EaseType.easeInOutQuart, "time", 5.0f));
        iTween.RotateTo(this.gameObject, iTween.Hash("rotation", new_rotation, "easetype", iTween.EaseType.easeInOutQuart, "time", 5.0f));

    }
    // Update is called once per frame
    private void Start()
    {
        MoveCamera(0);
    }

	private void Update()
	{
        // ���࿡ �ε����� 1�϶��� ���� ���� ������ ���� ���� �������ִ°��̴�.
        if (this.gameObject.transform.position == subCameras[1].transform.position)
        {
            // ���� ����ī�޶� �ε������� ī�޶� �̵��� �ߴٸ�,
            Debug.Log("�ܼ�");
            // ���̵��ض�.
            SceneManager.LoadScene("MainScene");
        }
    }
}
