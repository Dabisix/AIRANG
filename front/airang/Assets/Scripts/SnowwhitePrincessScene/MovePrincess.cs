using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePrincess : MonoBehaviour
{
    public AnimationCurve princessCurve;
    public Transform princessTransform;

    Vector3 princessStartPos;
    Vector3 princessTargetPos;

    public float timer;
    public float duration; //지속 시간동안 이동시킴
    public Vector3 vector;

    public PrincessAnimationController animController;
    public QueenController queenController;


    // Start is called before the first frame update
    void Start()
    {
        princessStartPos = princessTransform.position;
        princessTargetPos = princessStartPos + vector;
        //Invoke("ShowQueen", 3f);
    }

    public void ShowQueen()
	{
        queenController.SetQueenActive(true);
	}

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float percentage = timer / duration;
        princessTransform.position = Vector3.Lerp(princessStartPos, princessTargetPos, princessCurve.Evaluate(percentage));
        //애니메이터에 그 애니메이션을 실행시켜랑...
        //Time.timeScale = 0;
        //anim.SetBool("isCryingAfterWalking", princessTransform.position == princessTargetPos);

        //animController.SetIsCryingAfterWalking(princessTransform.position == princessTargetPos);
    }
}
