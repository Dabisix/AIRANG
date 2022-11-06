using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionScript : MonoBehaviour
{
    public string emotionName;
    public string eyesName;
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        animator.Play(emotionName);
        int eyesIndex = animator.GetLayerIndex("Eyes");
        animator.Play(eyesName, eyesIndex);
    }
}
