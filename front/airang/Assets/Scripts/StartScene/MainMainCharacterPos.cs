using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMainCharacterPos : MonoBehaviour
{
    public GameObject[] positions;
    // Start is called before the first frame update
    public void MainMoveCamera(int index)
    {
        Vector3 new_position = positions[index].transform.position;
        Vector3 new_rotation = positions[index].transform.eulerAngles;
        //transform.position = new_position;
        //transform.eulerAngles = new_rotation;

        iTween.MoveTo(this.gameObject, iTween.Hash("position", new_position, "easetype", iTween.EaseType.easeOutQuint, "time", 5.0f));
        iTween.RotateTo(this.gameObject, iTween.Hash("rotation", new_rotation, "easetype", iTween.EaseType.easeOutQuint, "time", 5.0f));


        if (index == 0)
        {
            bookListCanvas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
