using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMovementHandler : MonoBehaviour
{
    public float timeOfTravel = 5; //time after object reach a target place 
    float currentTime = 0; // actual floting time 
    float normalizedValue;

    public GameObject canvas; // container
    public GameObject panel; // moved object

    RectTransform rectTransform;

    public Vector3 to;
    public Vector3 from;

    private void Awake()
    {
        rectTransform = panel.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        StartCoroutine(LerpObject());
    }

    public void turnOff()
    {
        StartCoroutine(LerpBackObject());
    }

    public IEnumerator LerpObject()
    {
        currentTime = 0;
        while (currentTime <= timeOfTravel)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time 

            rectTransform.anchoredPosition = Vector3.Lerp(from, to, normalizedValue);
            yield return null;
        }
    }

    public IEnumerator LerpBackObject()
    {
        currentTime = 0;
        while (currentTime <= timeOfTravel)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time 

            rectTransform.anchoredPosition = Vector3.Lerp(to, from, normalizedValue);
            yield return null;
        }
        canvas.SetActive(false);
    }
}
