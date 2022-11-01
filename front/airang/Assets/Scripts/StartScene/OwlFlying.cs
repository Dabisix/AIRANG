using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlFlying : MonoBehaviour
{
    // Start is called before the first frame update
    public void MoveOwl()
    {
        Vector3 new_position = new Vector3(3.2f, 3, 12.6f);
        Vector3 new_rotation = new Vector3(-12.947f, -170.24f, 0);
        Vector3 new_scale = new Vector3(35, 35, 35);
        //transform.position = new_position;
        //transform.eulerAngles = new_rotation;

        iTween.MoveTo(this.gameObject, iTween.Hash("position", new_position, "easetype", iTween.EaseType.easeInOutBounce, "time", 5.0f));
        iTween.RotateTo(this.gameObject, iTween.Hash("rotation", new_rotation, "easetype", iTween.EaseType.easeInOutBounce, "time", 5.0f));
        iTween.ScaleTo(this.gameObject, iTween.Hash("scale", new_scale, "easetype", iTween.EaseType.easeInSine, "time", 5.0f));
    }

    // Update is called once per frame
    void Start()
    {
        MoveOwl();
    }
}
