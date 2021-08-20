using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintbrush : GrabbableObject
{
    public GameObject paintstrokePrefab;
    private GameObject spawnedPaint;
    private PaintbrushTip paintbrushTip;

    void Start()
    {
        paintbrushTip = GetComponentInChildren<PaintbrushTip>();
    }

    public override void OnInteractionStart()
    {
        // create the spawned paint
        spawnedPaint = Instantiate(paintstrokePrefab, paintbrushTip.transform.position, paintbrushTip.transform.rotation);

        TrailRenderer paintTrail = spawnedPaint.GetComponent<TrailRenderer>();
        paintTrail.material = paintbrushTip.paintMaterial;

    }

    public override void OnInteractionUpdating()
    {
        // make sure the spawned pain is following the paintbrush tip
        if (spawnedPaint)
        {
            spawnedPaint.transform.position = paintbrushTip.transform.position;
        }
    }

    public override void OnInteractionStop()
    {
        // stop following the paint brush tip
        // clear our spawned paint
        spawnedPaint.transform.position = spawnedPaint.transform.position;
        spawnedPaint = null;
    }
}
