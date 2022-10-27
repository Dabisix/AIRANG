using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCamera : MonoBehaviour
{
    public Camera[] subCameras;
    // 여기에 문 오브젝트를 저장을 해놓고, 만약 서브카메라1인놈의 move가 시작되면 그때 door를 여는게 나을것같다.
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
        // 만약에 인덱스가 1일때는 문을 열고 방으로 들어가고 씬을 변경해주는것이다.
        if (this.gameObject.transform.position == subCameras[1].transform.position)
        {
            // 만약 서브카메라 인덱스까지 카메라가 이동을 했다면,
            Debug.Log("콘솔");
            // 씬이동해라.
            SceneManager.LoadScene("MainScene");
        }
    }
}
