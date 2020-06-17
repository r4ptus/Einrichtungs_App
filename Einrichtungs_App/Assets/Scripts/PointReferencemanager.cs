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
        FindObjectOfType<PlacementIndicator>().gameObject.transform.GetChild(0).localScale = referencePointManager.anchorPrefab.gameObject.transform.localScale;
    }
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began)
            return;

        if (aRRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            ARAnchor referencePoint = referencePointManager.AddAnchor(hitPose);

            if (referencePoint == null)
                Debug.Log("There was an error creating the referencPoint");
            else
                referencePoints.Add(referencePoint);
        }
    }
}
