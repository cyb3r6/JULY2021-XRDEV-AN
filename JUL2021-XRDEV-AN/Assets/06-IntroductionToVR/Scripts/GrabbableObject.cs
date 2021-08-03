using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public Color hoverColor;
    public Color nonHoverColor;

    private Material material;
    public Rigidbody grabbedRigidbody;

    void Awake()
    {
        material = GetComponent<Renderer>().material;
        grabbedRigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// When initial collision happens
    /// change color to indicate we're hovering over
    /// a grabbable object
    /// </summary>
    public void OnHoverStarted()
    {
        material.color = hoverColor;
    }

    /// <summary>
    /// To indicate we've stopped hovering 
    /// over a grabbable obejct
    /// </summary>
    public void OnHoverEnded()
    {
        material.color = nonHoverColor;
    }

    /// <summary>
    /// Parenting method of grabbing.
    /// The parent is the controller doing 
    /// the grabbing
    /// </summary>
    /// <param name="controller"></param>
    public void Grab(VRinput controller)
    {
        transform.SetParent(controller.transform);
        grabbedRigidbody.useGravity = false;
        grabbedRigidbody.isKinematic = true;
    }

    /// <summary>
    /// Parenting method of releasing
    /// The parent is null
    /// </summary>
    /// <param name="controller"></param>
    public void Release(VRinput controller)
    {
        transform.SetParent(null);
        grabbedRigidbody.useGravity = true;
        grabbedRigidbody.isKinematic = false;
    }

    /// <summary>
    /// Joint method of grabbing
    /// Create a fixed joint between the controller
    /// and grabbed object.
    /// The connected body is the grabbed rigidbody
    /// </summary>
    /// <param name="controller"></param>
    public void JointGrab(VRinput controller)
    {
        FixedJoint fx = controller.gameObject.AddComponent<FixedJoint>();
        fx.connectedBody = grabbedRigidbody;
    }

    /// <summary>
    /// Joint method of released
    /// Destroying the fixed joint between the controller
    /// and grabbed object
    /// </summary>
    /// <param name="controller"></param>
    public void JointRelease(VRinput controller)
    {
        FixedJoint fx = controller.GetComponent<FixedJoint>();
        Destroy(fx);
    }
}
