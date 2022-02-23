using UnityEngine;

public class RemoveObject : MonoBehaviour
{
    void Update()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.current.ScreenPointToRay(touch.position);
            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject))
            {
                Destroy(hitObject.transform.parent.transform.parent.gameObject);
            }
        }       
    }
}
