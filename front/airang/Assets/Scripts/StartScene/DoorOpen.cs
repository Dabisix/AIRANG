using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openDoor()
	{
        Vector3 new_rotation = new Vector3(0, 150.516f, 0);

        // 문을 열려면 회전을 하여라~
        iTween.RotateTo(this.gameObject, iTween.Hash("rotation", new_rotation, "easetype", iTween.EaseType.easeInOutQuart, "time", 5.0f));
    }
}
