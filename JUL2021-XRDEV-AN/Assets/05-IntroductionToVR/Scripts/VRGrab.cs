using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls when the controllers grab
/// </summary>
public class VRGrab : MonoBehaviour
{
    public GrabbableObject collidingObject;
    public GrabbableObject heldObject;

    private VRinput controller;


    /// <summary>
    /// Initiate hovering over grabbable object
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        var grab = other.GetComponent<GrabbableObject>();
        if (grab)
        {
            collidingObject = grab;
            collidingObject.OnHoverStarted();
        }
    }

    /// <summary>
    /// Initiate stop hovering over grabbable object
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        var grab = other.GetComponent<GrabbableObject>();
        if (grab == collidingObject)
        {
            collidingObject.OnHoverEnded();
            collidingObject = null;
        }
    }

    void Awake()
    {
        controller = GetComponent<VRinput>();

        controller.OnGripDown.AddListener(Grab);
        controller.OnGripUp.AddListener(Release);
    }

    /// <summary>
    /// Initializing grabbing the held obejct
    /// </summary>
    public void Grab()
    {
        if(collidingObject != null)
        {
            heldObject = collidingObject;
            heldObject.JointGrab(controller);

            // interactions
            controller.OnTriggerDown.AddListener(heldObject.OnInteractionStart);
            controller.OnTriggerUp.AddListener(heldObject.OnInteractionStop);
            controller.OnTriggerUpdating.AddListener(heldObject.OnInteractionUpdating);
        }
    }

    /// <summary>
    /// Initializing dropping the held object
    /// </summary>
    public void Release()
    {
        if (heldObject != null)
        {
            heldObject.JointRelease(controller);

            // throw
            heldObject.grabbedRigidbody.velocity = controller.velocity;
            heldObject.grabbedRigidbody.angularVelocity = controller.angularVelocity;

            // interactions
            controller.OnTriggerDown.RemoveListener(heldObject.OnInteractionStart);
            controller.OnTriggerUp.RemoveListener(heldObject.OnInteractionStop);
            controller.OnTriggerUpdating.RemoveListener(heldObject.OnInteractionUpdating);

        }
    }
   
}
