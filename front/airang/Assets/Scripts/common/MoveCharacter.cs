using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class MoveCharacter : MonoBehaviour

{
    [SerializeField]
    public Animator anim;
    [SerializeField]
    Rigidbody rigidbody;
    [SerializeField]
    private float jumpForce = 7f;
    //public float speed = 10.0f;
    float hAxis;
    float vAxis;
    public float speed;
    Vector3 moveVec;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()

    {
        // Input Manager

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;


        anim.SetInteger("actionID", moveVec != Vector3.zero ? 2 : 1);
        transform.LookAt(transform.position + moveVec);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 이 파라미터를 이용해서 부딪힌 대상에 대한 함수를 실행할 수 있다. 없앤다거나, 따로 UI를 띄운다거나...
        Debug.Log("충돌 감지!");

    }

}