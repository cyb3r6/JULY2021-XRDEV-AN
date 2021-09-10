using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRaycaster : MonoBehaviour
{
    [SerializeField]
    private Transform director;

    [SerializeField]
    private LineRenderer lineRenderer;

    public float maxDistance = 10f;

    public int layerMask = 5;

    private VRinput controller;

    private Button button;


    
    void Start()
    {
        controller = GetComponent<VRinput>();
        controller.OnTriggerDown.AddListener(VRButtonClick);
    }

    
    void Update()
    {
        // << is shifting bits to the left (up, so)
        // if we have 0000000000000001 we're turning it into
        //            0000000000100000
        layerMask = 1 << 5;

        RaycastHit hit;
        if (Physics.Raycast(controller.transform.position, director.transform.forward, out hit, maxDistance, layerMask))
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, director.transform.position);
            lineRenderer.SetPosition(1, hit.point);

            button = hit.transform.GetComponent<Button>();
        }
        else
        {
            lineRenderer.enabled = false;
            button = null;
        }
    }

    public void VRButtonClick()
    {
        if (button)
        {
            button.onClick.Invoke();
        }
    }
}
