using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    private static ARRaycastManager raycastManager;
    private static GameObject visual;
    public static GameObject lastSelectedObject;
    private static Vector2 touchPosition;
    private static Camera _Camera;

    public static bool intersected;
    public static bool selected;
    // Start is called before the first frame update
    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;
        visual.SetActive(false);
        _Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if (!visual.activeInHierarchy)
            {
                visual.SetActive(true);
            }


        }


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Began");
                //selected = true;
                Ray ray = _Camera.ScreenPointToRay(touch.position);
                Debug.Log(ray);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                  {
                      Debug.Log(hitObject);
                    if (hitObject.transform.gameObject.name == "Cube")
                    {
                       lastSelectedObject = hitObject.transform.gameObject;
                       selected = true;
                    }
                      
                }
                
            }


            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Ended");
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Cube")
        {
            intersected = true;
        }


    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "Cube")
        {
            intersected = false;
        }
    }
}
