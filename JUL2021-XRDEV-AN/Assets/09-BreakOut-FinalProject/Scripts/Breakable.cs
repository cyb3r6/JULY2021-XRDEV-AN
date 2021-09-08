using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField]
    private Rigidbody breakableRigidBody;

    [SerializeField]
    private GameObject brokenObject;

    [SerializeField]
    private GameObject unbrokenObject;
    
    void Start()
    {
        
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if(breakableRigidBody.velocity.magnitude > 1.35)
        {
            unbrokenObject.SetActive(false);
            brokenObject.SetActive(true);

            Rigidbody[] rigidbodies = brokenObject.GetComponentsInChildren<Rigidbody>();

        }
    }
}
