using System;
using UnityEngine;
using UnityEngine.UI;
 
public class RotateObject : MonoBehaviour
{
    // Assign in the inspector
    private GameObject objectToRotate;
    public Slider slider;
 
    // Preserve the original and current orientation
    private float previousValue;
    private float startValue = 0.5f;
    void Awake ()
    {
        // Assign a callback for when this slider changes
        slider.onValueChanged.AddListener (OnSliderChanged);
 
        // And current value
        previousValue = slider.value;
    }
 
    void OnSliderChanged (float value)
    {
        // How much we've changed
        float delta = value - previousValue;
        objectToRotate.transform.Rotate (Vector3.up * delta * 360);
 
        // Set our previous value for the next change
        previousValue = value;
    }
    
    void Update()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.current.ScreenPointToRay(touch.position);
            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject))
            {
                objectToRotate = hitObject.transform.parent.transform.parent.gameObject;
                objectToRotate.GetComponent<Recolour>().SetSelected();
            }
        }       
    }

    public void Deselect()
    {
        objectToRotate.GetComponent<Recolour>().SetOriginalMaterial();
        objectToRotate = null;
        slider.value = startValue;
    }
}