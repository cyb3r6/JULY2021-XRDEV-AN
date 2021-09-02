using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimHandGrab : MonoBehaviour
{
    public GrabbableObject collidingObject;
    public GrabbableObject heldObject;

    private VRinput controller;
    private Animator handAnimator;
    private void OnTriggerEnter(Collider other)
    {
        var grab = other.GetComponent<GrabbableObject>();
        if (grab)
        {
            collidingObject = grab;
            collidingObject.OnHoverStarted();
        }
    }

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
        handAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Grab();
            handAnimator.Play("Fist Closing", 0, 1);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Release();
            handAnimator.Play("Fist Closing", 0, 0.1f);
        }
    }

    public void Grab()
    {
        if (collidingObject != null)
        {
            heldObject = collidingObject;
            heldObject.Grab(controller);
        }
    }

    public void Release()
    {
        if (heldObject != null)
        {
            heldObject.Release(controller);
        }
    }
}
