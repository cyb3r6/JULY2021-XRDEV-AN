using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burganator : GrabbableObject
{
    public Transform spawnPoint;
    public float shootingForce;
    public GameObject foodPrefab;

    public override void OnInteractionStart()
    {
        base.OnInteractionStart();

        GameObject shot = Instantiate(foodPrefab, spawnPoint.position, spawnPoint.rotation);

        shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * shootingForce);

        Destroy(shot, 3);
    }
}
