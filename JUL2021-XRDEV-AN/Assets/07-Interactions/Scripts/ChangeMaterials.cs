using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterials : MonoBehaviour
{
    public Material pressedMaterial;
    public Material originalMaterial;

    public List<GameObject> objects;
    private bool switchMaterial = false;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            objects.Add(transform.GetChild(i).gameObject);
        }
    }


    public void DoChangeMaterials()
    {
        switchMaterial = !switchMaterial;

        if(switchMaterial == true)
        {
            for(int i = 0; i < objects.Count; i++)
            {
                objects[i].GetComponent<Renderer>().material = pressedMaterial;
            }
        }
        else
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].GetComponent<Renderer>().material = originalMaterial;
            }
        }
    }
}
