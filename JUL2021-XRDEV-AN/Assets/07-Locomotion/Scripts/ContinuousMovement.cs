using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousMovement : MonoBehaviour
{

    public Transform xrRig;
    public Transform director;

    private VRinput controller;
    private Vector3 playerForward;
    private Vector3 playerRight;
    
    void Awake()
    {
        controller = GetComponent<VRinput>();
    }

    
    void Update()
    {
        playerForward = director.forward;
        playerForward.y = 0;
        playerForward.Normalize();

        playerRight = director.right;
        playerRight.y = 0;
        playerRight.Normalize();

        xrRig.position += playerForward * controller.thumbstick.y * Time.deltaTime;
        xrRig.position += playerRight * controller.thumbstick.x * Time.deltaTime;
    }
}
