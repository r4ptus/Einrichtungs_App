using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class MoveButtonScript : MonoBehaviour
{
    private PointReferencemanager PointReferencemanager;
    private static bool left, right, move;
    private static ARRaycastManager raycastManager;
    private ARAnchorManager referencePointManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public PlacementIndicator PlacementIndicator;
    //private static GameObject dummy;
    // Start is called before the first frame update
    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        PointReferencemanager = GetComponent<PointReferencemanager>();
        referencePointManager = GetComponent<ARAnchorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (left)
        {
            GameObject selectedObject = PlacementIndicator.lastSelectedObject;
            selectedObject.transform.Rotate(0, -0.5f, 0);
        }
        if (right)
        {
            GameObject selectedObject = PlacementIndicator.lastSelectedObject;
            selectedObject.transform.Rotate(0, 0.5f, 0);
        }
        if (move)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon);
           
            if (hits.Count > 0)
            {
                if (PlacementIndicator.lastSelectedObject != null && FindObjectOfType<PlacementIndicator>().gameObject.transform.childCount<2)
                {
                    //Debug.Log("not null");
                    GameObject selectedObject = PlacementIndicator.lastSelectedObject;
                    FindObjectOfType<PlacementIndicator>().gameObject.transform.GetChild(0).localScale = new Vector3(selectedObject.transform.localScale.x, 0, selectedObject.transform.localScale.y);
                   
                    Instantiate(selectedObject, FindObjectOfType<PlacementIndicator>().gameObject.transform.GetChild(0).transform.position, FindObjectOfType<PlacementIndicator>().gameObject.transform.GetChild(0).transform.rotation, FindObjectOfType<PlacementIndicator>().gameObject.transform).SetActive(true);
                   
                    Debug.Log(FindObjectOfType<PlacementIndicator>().gameObject.transform.childCount);
                
                    Destroy(PlacementIndicator.lastSelectedObject);
                    Debug.Log(PlacementIndicator.lastSelectedObject);
                }
             


            }
        }
       
            
                
        
    }

    public void RotateLeft()
    {
        if (left)
        {
            left = false;
        }
        else
        {
            left = true;
        }

    }
    public void RotateRight()
    {
        if (right)
        {
            right = false;
        }
        else
        {
            right = true;
        }

    }
    public void Delete()
    {
        GameObject selectedObject = PlacementIndicator.lastSelectedObject;
        Destroy(selectedObject);
        PlacementIndicator.selected = false;
    }
    public void Move()
    {
        if (move)
        {
            move = false;
            Destroy(FindObjectOfType<PlacementIndicator>().gameObject.transform.GetChild(1).gameObject);
            FindObjectOfType<PointReferencemanager>().OnButtonCLick();


            PlacementIndicator.selected = false;

        }
        else
        {
            move = true;
        }
    }

}
