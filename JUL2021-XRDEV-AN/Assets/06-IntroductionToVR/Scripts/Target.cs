using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    /// <summary>
    /// How fast the cube is moving
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// The maximum/minimum amount of distance 
    /// the cube moves
    /// </summary>
    public float moveAmount;

    /// <summary>
    /// Controls the spawning of new targets
    /// </summary>
    public SpawnArea spawnArea;

    /// <summary>
    /// Saving the starting posiiton of the target
    /// </summary>
    private float startingPosition;


    void Awake()
    {
        startingPosition = transform.position.x;
    }

    
    void Update()
    {
        var newPosition = transform.position;
        newPosition.x = startingPosition + Mathf.Sin(Time.time * moveSpeed) * moveAmount;
        transform.position = newPosition;
    }

    /// <summary>
    /// When collided with food thrown at it
    /// this will destroy itself and call the
    /// SpawnArea to spawn anther target
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        var foodstuff = collision.gameObject.GetComponent<GrabbableObject>();

        if(foodstuff != null)
        {
            Debug.Log("Hit");
            Destroy(foodstuff.gameObject);
            Destroy(this.gameObject);

            spawnArea.SpawnTarget();
        }
    }
}
