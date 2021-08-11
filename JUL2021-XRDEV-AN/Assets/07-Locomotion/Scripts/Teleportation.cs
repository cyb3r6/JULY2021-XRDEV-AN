using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform xrRig;

    private Vector3 hitPosition;
    private LineRenderer line;
    private VRinput controller;
    private bool shouldTeleport = false;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        controller = GetComponent<VRinput>();

        controller.ThumbstickButtonUpdating.AddListener(RaycastLine);
        controller.ThumbstickButtonUp.AddListener(Teleport);
    }

    public void RaycastLine()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit))
        {
            hitPosition = hit.point;
            line.SetPosition(0, controller.transform.position);
            line.SetPosition(1, hitPosition);
            line.enabled = true;
            shouldTeleport = true;
        }

    }

    public void Teleport()
    {
        if(shouldTeleport == true)
        {
            xrRig.position = hitPosition;
            shouldTeleport = false;
            line.enabled = false;
        }
    }
}
