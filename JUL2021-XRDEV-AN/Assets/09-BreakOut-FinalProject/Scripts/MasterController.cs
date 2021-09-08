using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MasterController : MonoBehaviour
{
    public static MasterController instance;
    public Transform startingPosition;

    public float maxScale = 3f;
    public float minScale = 0.3f;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        transform.position = startingPosition.position;
        transform.rotation = startingPosition.rotation;
    }

    public void Shrink()
    {
        transform.DOScale(minScale, 2f);
    }

    public void Grow()
    {
        transform.DOScale(maxScale, 2f);
    }

    public void ReturnToNormal()
    {
        transform.DOScale(Vector3.one, 2f);
    }

}
