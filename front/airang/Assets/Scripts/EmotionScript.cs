using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionScript : MonoBehaviour
{
    public string emotionName;
    public string eyesName;
    public float speed; //1이 기본속도

    // Start is called before the first frame update
    void Start()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.Play(emotionName);
        int eyesIndex = animator.GetLayerIndex("Eyes");
        animator.Play(eyesName, eyesIndex);
        if(speed > 0)
            animator.speed = speed;
    }
}
