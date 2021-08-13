using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform xrRig;
    public float midPointHeight = 2f;
    public int lineResolution = 10;
    public float smoothAmount = 3f;
    public GameObject teleportReticleprefab;

    private GameObject reticle;
    private Vector3 hitPosition;
    private Vector3 lastHitPosition;
    private Vector3 smoothedEndPosition;
    private LineRenderer line;
    private VRinput controller;
    private bool shouldTeleport = false;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = lineResolution + 1;

        controller = GetComponent<VRinput>();

        controller.ThumbstickButtonUpdating.AddListener(RaycastLine);
        controller.ThumbstickButtonUp.AddListener(Teleport);

        if(reticle == null)
        {
            reticle = Instantiate(teleportReticleprefab);
        }

        reticle.SetActive(false);
    }

    public void RaycastLine()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit))
        {
            hitPosition = hit.point;
            //line.SetPosition(0, controller.transform.position);
            //line.SetPosition(1, hitPosition);

            Vector3 hitPositionDirection = (hitPosition - lastHitPosition) / smoothAmount;

            smoothedEndPosition = lastHitPosition + hitPositionDirection;

            CurveLine(smoothedEndPosition);
            lastHitPosition = smoothedEndPosition;

            // teleport visuals
            line.enabled = true;
            shouldTeleport = true;
            reticle.transform.position = smoothedEndPosition;
            reticle.transform.LookAt(hit.normal + smoothedEndPosition);
            reticle.SetActive(true);
            
        }

    }

    public void Teleport()
    {
        if(shouldTeleport == true)
        {
            xrRig.position = hitPosition;
            shouldTeleport = false;
            line.enabled = false;
            reticle.SetActive(false);
        }
    }

    private void CurveLine(Vector3 hitPoint)
    {
        Vector3 startPoint = controller.transform.position;
        Vector3 endPoint = hitPoint;
        Vector3 midPoint = (endPoint - startPoint) / 2 + startPoint;

        // means midPoint.y = midPoint.y + midPointHeight
        midPoint.y += midPointHeight; 


        for(int i = 0; i <= lineResolution; i++)
        {
            float t = (float)i / (float)lineResolution;

            Vector3 startToMid = Vector3.Lerp(startPoint, midPoint, t);
            Vector3 midToEnd = Vector3.Lerp(midPoint, endPoint, t);
            Vector3 curvePosition = Vector3.Lerp(startToMid, midToEnd, t);

            line.SetPosition(i, curvePosition);

        }
    }
}
