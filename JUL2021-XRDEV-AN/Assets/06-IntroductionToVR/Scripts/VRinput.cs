using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRinput : MonoBehaviour
{
    public Hand hand = Hand.Left;

    public float gripValue;
    public Vector3 velocity;
    public Vector3 angularVelocity;

    private string gripAxis;
    private string gripButton;
    private Vector3 previousPosition;
    private Vector3 previousAngularPosition;

    private Animator handAnimator;

    public UnityEvent OnGripDown;
    public UnityEvent OnGripUp;


    void Start()
    {
        handAnimator = GetComponentInChildren<Animator>();

        gripAxis = $"XRI_{hand}_Grip";
        gripButton = $"XRI_{hand}_GripButton";
    }

    
    void Update()
    {
        gripValue = Input.GetAxis(gripAxis);

        // controller velocity
        velocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = this.transform.position;
        // controller angular velocity
        angularVelocity = (transform.eulerAngles - previousAngularPosition) / Time.deltaTime;
        previousAngularPosition = transform.eulerAngles;

        // animating the hand using normalized time
        if(handAnimator != null)
        {
            handAnimator.Play("Fist Closing", 0, gripValue);
        }

        if (Input.GetButtonDown(gripButton))
        {
            OnGripDown?.Invoke();
        }
        if (Input.GetButtonUp(gripButton))
        {
            OnGripUp?.Invoke();
        }
    }
}

[System.Serializable]
public enum Hand
{
    Left,
    Right
}