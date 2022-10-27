using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneControl : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text PlaneText;
    [SerializeField] TMPro.TMP_Text StateText;
    [SerializeField] ARPlaneManager PlaneManager;

    public int PlaneCount { get; }

    //Linee possibili
    // List<ARPlane> CurrentPlane = new List<ARPlane>();
    void Update()
    {
        ARSession.stateChanged += OnStateChanged;

        PlaneManager.planesChanged += CountPlane;
    }

    void OnStateChanged(ARSessionStateChangedEventArgs e)
    {
        StateText.SetText("state changed to : " + e.state);
    }

    void CountPlane(ARPlanesChangedEventArgs c)
    {
        PlaneText.SetText("Plane and points detected " + PlaneCount);
    }
}
