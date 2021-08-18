using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableTrees : MonoBehaviour
{
    public Lever upDownLever;
    public float speed = 2f;
    
   
    void Update()
    {
        if(Mathf.Abs(upDownLever.NormalizedAngle()) > 0.15f)
        {
            transform.position = transform.position + transform.up * Time.deltaTime * speed * upDownLever.NormalizedAngle();
        }
    }
}
