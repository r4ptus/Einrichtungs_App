using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARAnchorManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class PointReferencemanager : MonoBehaviour
{
    private ARRaycastManager aRRaycastManager;
    private ARAnchorManager referencePointManager;
    private ARPlaneManager planeManager;

    private List<ARAnchor> referencePoints = new List<ARAnchor>();

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        referencePointManager = GetComponent<ARAnchorManager>();
        planeManager = GetComponent<ARPlaneManager>();
        if(referencePointManager.anchorPrefab.gameObject != null)
            FindObjectOfType<PlacementIndicator>().gameObject.transform.GetChild(0).localScale = referencePointManager.anchorPrefab.gameObject.transform.localScale;


    }
    private void OnEnable()
    {
        PlacementBehaviour.buttonClickDelegate += OnButtonCLick;
        Debug.Log("OnEnable");
        if(PlacementBehaviour.buttonClickDelegate != null)
            Debug.Log(PlacementBehaviour.buttonClickDelegate.ToString());
    }
    private void OnDisable()
    {
        PlacementBehaviour.buttonClickDelegate += OnButtonCLick;
        Debug.Log("OnDisable");

    }
    public void OnButtonCLick()
    {

        if (aRRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            // testen ob Object vorhanden, schauen ob Raycast auf Object zeigt
            Pose hitPose = hits[0].pose;
            ARAnchor referencePoint = referencePointManager.AddAnchor(hitPose);
            referencePoint.name = "Cube";
            Debug.Log("AR");

            if (referencePoint == null)
                Debug.Log("There was an error creating the referencPoint");
            else
                referencePoints.Add(referencePoint);
        }
    }
}
