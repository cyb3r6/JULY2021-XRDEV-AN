using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public GameObject plug;
    public ParticleSystem particleSystemLiquid;
    public ParticleSystem particleSystemSplash;
    public float fillAmount = 0.8f;
    public GameObject destroyedBottle;
    public Rigidbody bottleRigidbody;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(bottleRigidbody.velocity.magnitude > 1.35)
        {
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            if(particleSystemSplash != null)
            {
                particleSystemSplash.gameObject.SetActive(true);
                if(fillAmount > 0)
                {
                    particleSystemSplash.Play();
                }
            }
            destroyedBottle.SetActive(true);
        }
    }
}
