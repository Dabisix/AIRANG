using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAnimation : MonoBehaviour
{
    public AnimationCurve rabbitCurve;
    public Transform rabbitTransform;

    Vector3 rabbitStartPos;
    Vector3 rabbitTargetPos;

    public float timer;
    public float duration; //¡ˆº” Ω√∞£µøæ» ¿ÃµøΩ√≈¥
    public Vector3 vector;

    // Start is called before the first frame update
    void Start()
    {
        rabbitStartPos = rabbitTransform.position;
        rabbitTargetPos = rabbitStartPos + vector;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float percentage = timer / duration;
        if (rabbitTransform.position == rabbitTargetPos)
        {
            Debug.Log("≈‰≥¢∞° ∏ÿ√·¥Á");
            Time.timeScale = 0;
        }
        else
        {
            rabbitTransform.position = Vector3.Lerp(rabbitStartPos, rabbitTargetPos, rabbitCurve.Evaluate(percentage));
        }
    }
}
