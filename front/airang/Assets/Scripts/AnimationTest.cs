using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    public AnimationCurve curve;

    Vector3 startPos;
    Vector3 targetPos;
    float timer = 0.0f;
    float duration = 10f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3(50, 0, 0);
        targetPos = new Vector3(0, 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float percentage = timer / duration;
        transform.position = Vector3.Lerp(startPos, targetPos, percentage);
    }
}
