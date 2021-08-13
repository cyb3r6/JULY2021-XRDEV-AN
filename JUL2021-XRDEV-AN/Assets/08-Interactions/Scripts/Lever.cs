using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : GrabbableObject
{
    public Vector3 centerOfMass = Vector3.zero;
    
    void Awake()
    {
        grabbedRigidbody.centerOfMass = centerOfMass;
    }
}
