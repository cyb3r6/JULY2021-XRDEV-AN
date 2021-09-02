using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintbrushTip : MonoBehaviour
{
    public Material originalMaterial;
    public Material paintMaterial;

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Paint")
        {
            paintMaterial = collision.collider.GetComponent<Renderer>().material;
            this.GetComponent<Renderer>().material = paintMaterial;
        }
    }

}
