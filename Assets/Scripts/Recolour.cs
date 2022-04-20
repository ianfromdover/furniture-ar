using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Recolour : MonoBehaviour
{
    public Material valid;
    public Material invalid;
    public Material selected;
    private List<Material> originalMaterials = new List<Material>();
    private List<GameObject> children = new List<GameObject>();
    void Start()
    {
        if (gameObject.GetComponent<MeshRenderer>() != null) // has no children
        {
            originalMaterials.Add(gameObject.GetComponent<MeshRenderer>().material);
        }
        else
        {
            foreach(Transform child in transform.GetChild(0).transform)
            {
                Debug.Log(child.gameObject.name);
                children.Add(child.gameObject);
                originalMaterials.Add(child.GetComponent<MeshRenderer>().material);
            }
        }
    }
    public void SetValid()
    {
        SetMaterial(valid);
    }
    public void SetInvalid()
    {
        SetMaterial(invalid);
    }
    public void SetSelected()
    {
        SetMaterial(selected);
    }
    
    private bool SetMaterial(Material m)
    {
        if (gameObject.GetComponent<MeshRenderer>() != null) // has no children
        {
            gameObject.GetComponent<MeshRenderer>().material = m;
        }
        else
        {
            foreach(GameObject child in children)
            {
                child.GetComponent<MeshRenderer>().material = m;
            }
        }
        return true;
    }
    public bool SetOriginalMaterial()
    {
        if (gameObject.GetComponent<MeshRenderer>() != null) // has no children
        {
            gameObject.GetComponent<MeshRenderer>().material = originalMaterials[0];
        }
        else
        {
            int i = 0;
            foreach(GameObject child in children)
            {
                child.GetComponent<MeshRenderer>().material = originalMaterials[i];
                i++;
            }
        }
        return true;
    }
}
