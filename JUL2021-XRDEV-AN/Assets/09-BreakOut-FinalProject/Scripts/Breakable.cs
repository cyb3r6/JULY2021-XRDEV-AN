using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Breakable : MonoBehaviour
{
    [SerializeField]
    private Rigidbody breakableRigidBody;

    [SerializeField]
    private GameObject brokenObject;

    [SerializeField]
    private GameObject unbrokenObject;

    public UnityEvent OnBreak;

    

    private void OnCollisionEnter(Collision collision)
    {
        if(breakableRigidBody.velocity.magnitude > 1.35)
        {
            unbrokenObject.SetActive(false);
            brokenObject.SetActive(true);

            OnBreak?.Invoke();

            Rigidbody[] rigidbodies = brokenObject.GetComponentsInChildren<Rigidbody>();

        }
    }
}
