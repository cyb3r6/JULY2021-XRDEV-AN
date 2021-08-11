using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    /// <summary>
    /// The target prefab in the prefabs folder
    /// </summary>
    public Target targetPrefab;

    /// <summary>
    /// Spawn area collider
    /// </summary>
    public BoxCollider spawnAreaCollider;
   
    void Start()
    {
        SpawnTarget();
    }

    /// <summary>
    /// Instantiate a new target and assigning
    /// the spawn aread to this!
    /// </summary>
    public void SpawnTarget()
    {
        var newTarget = Instantiate(targetPrefab, GetRandomPosition(), targetPrefab.transform.rotation);

        newTarget.spawnArea = this;
    }

    /// <summary>
    /// Method to return a random position using box collider
    /// min and max bounds
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomPosition()
    {
        var xValue = Random.Range(spawnAreaCollider.bounds.min.x, spawnAreaCollider.bounds.max.x);
        var yValue = Random.Range(spawnAreaCollider.bounds.min.y, spawnAreaCollider.bounds.max.y);
        var zValue = Random.Range(spawnAreaCollider.bounds.min.z, spawnAreaCollider.bounds.max.z);

        return new Vector3(xValue, yValue, zValue);
    }
}
