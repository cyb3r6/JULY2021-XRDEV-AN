using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public string potionType = "Default";
    public ParticleSystem particleSystemLiquid;
    public ParticleSystem particleSystemSplash;
    public float fillAmount = 0.8f;
    public GameObject destroyedBottle;
    public Rigidbody bottleRigidbody;
    public MeshRenderer meshRenderer;
    private MaterialPropertyBlock materialPropertyBlock;
    private float startingFillAmount;
    
    void OnEnable()
    {
        materialPropertyBlock = new MaterialPropertyBlock();
        materialPropertyBlock.SetFloat("LiquidFill", fillAmount);

        meshRenderer.SetPropertyBlock(materialPropertyBlock);

        startingFillAmount = fillAmount;
    }

    
    void Update()
    {
        if(Vector3.Dot(transform.up, Vector3.down)> 0 && fillAmount > 0)
        {
            if (particleSystemLiquid.isStopped)
            {
                particleSystemLiquid.Play();
            }

            fillAmount -= 0.75f * Time.deltaTime;

            // detect if we're pouring into our potion receiver
            RaycastHit hit;
            if(Physics.Raycast(particleSystemLiquid.transform.position, Vector3.down, out hit, 50.0f, ~0, QueryTriggerInteraction.Collide))
            {
                PotionReceiver receiver = hit.collider.GetComponent<PotionReceiver>();
                if(receiver != null)
                {
                    receiver.ReceivePotion(potionType);
                }
            }
        }
        else
        {
            particleSystemLiquid.Stop();
        }

        meshRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetFloat("LiquidFill", fillAmount);
        meshRenderer.SetPropertyBlock(materialPropertyBlock);

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
