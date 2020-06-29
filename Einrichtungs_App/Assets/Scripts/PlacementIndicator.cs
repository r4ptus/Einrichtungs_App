using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private GameObject visual;
    public static bool intersected;
    // Start is called before the first frame update
    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;
        visual.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon);

        Debug.Log(intersected);
        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
            
            if (!visual.activeInHierarchy)
            {
                visual.SetActive(true);
            }
            
                
        }
    }

     void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "Cube")
        {
            Debug.Log("Collision enter");
            intersected = true;
        }
       
       
    }

    private void OnCollisionExit(Collision col)
    {
        
        if (col.gameObject.name == "Cube")
        {
            Debug.Log("Collision exit");
            intersected = false;
        }
    }
}
