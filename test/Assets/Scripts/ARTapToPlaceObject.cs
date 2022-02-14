using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject placementIndicator;
    public GameObject objectToPlace;

    private Pose PlacementPose; // contains a Vector3 for a position and a quaternion for rotation
    private ARRaycastManager aRRaycastManager;
    private bool placementPostIsValid = false;

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPostIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    // turn the indicator on or off
    private void UpdatePlacementIndicator()
    {
        if (placementPostIsValid) // works
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }
    
    // does raycast to center of screen, looks for planes, and stores the results in hits.
    // then if there are hits, set placementPose to that pose.
    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPostIsValid = hits.Count > 0;
        if (placementPostIsValid)
        {
            PlacementPose = hits[0].pose;
        }
    }

    // Next section
    private void PlaceObject()
    {
        Instantiate(objectToPlace, PlacementPose.position, PlacementPose.rotation);
    }
}
