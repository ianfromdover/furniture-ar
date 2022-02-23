using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject ghost;
    public GameObject objectToPlace;
    
    // furniture
    public GameObject chair;
    public GameObject table;
    public GameObject pouf;
    public GameObject shelf;
    public GameObject sofa;

    private Pose PlacementPose; // contains a Vector3 for a position and a quaternion for rotation
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid) // works
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

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;
        }
    }

    // Next section
    public void PlaceObject()
    {
        if (placementPoseIsValid)
        {
            ghost.GetComponent<Recolour>().SetOriginalMaterial();
            ghost.transform.parent = null;
            ghost = Instantiate(objectToPlace, PlacementPose.position, PlacementPose.rotation);
            ghost.GetComponent<Recolour>().SetValid();
            ghost.transform.parent = placementIndicator.transform;
        }
    }
    
    private void UseObject(GameObject o)
    {
        objectToPlace = o;
        Destroy(ghost);
        ghost = Instantiate(o, PlacementPose.position, PlacementPose.rotation);
        ghost.GetComponent<Recolour>().SetValid();
        ghost.transform.parent = placementIndicator.transform;
    }
    
    public void UseChair()
    {
        UseObject(chair);
    }
    
    public void UseTable()
    {
        UseObject(table);
    }
    
    public void UsePouf()
    {
        UseObject(pouf);
    }
    
    public void UseShelf()
    {
        UseObject(shelf);
    }
    
    public void UseSofa()
    {
        UseObject(sofa);
    }
}
