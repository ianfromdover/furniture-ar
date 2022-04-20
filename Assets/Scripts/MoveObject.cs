using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MoveObject : MonoBehaviour
{
    // private ARRaycastManager aRRaycastManager;
    public Camera aRCamera;

    private GameObject objectToMove;

    void Update()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.current.ScreenPointToRay(touch.position);
            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject))
            {
                objectToMove = hitObject.transform.parent.transform.parent.gameObject;
                objectToMove.GetComponent<Recolour>().SetSelected();
                objectToMove.transform.parent = aRCamera.transform;
            }
        }       
    }

    public void Deselect()
    {
        objectToMove.GetComponent<Recolour>().SetOriginalMaterial();
        objectToMove.transform.parent = null;
        objectToMove = null;
    }
}
