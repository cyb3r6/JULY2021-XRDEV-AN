using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : GrabbableObject
{
    public Vector3 centerOfMass = Vector3.zero;
    public HingeJoint leverJoint;
    
    void Awake()
    {
        grabbedRigidbody.centerOfMass = centerOfMass;
    }

    public float NormalizedAngle()
    {
        float normalizedAngle = leverJoint.angle / leverJoint.limits.max;
        return normalizedAngle;
    }
}
