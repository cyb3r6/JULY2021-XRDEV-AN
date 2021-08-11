using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRinput : MonoBehaviour
{
    public Hand hand = Hand.Left;

    public float gripValue;
    public float triggerValue;
    public Vector3 velocity;
    public Vector3 angularVelocity;
    public Vector2 thumbstick;

    private string gripAxis;
    private string triggerAxis;
    private string gripButton;
    private string triggerButton;
    private string thumbstickX;
    private string thumbstickY;
    private string thumbstickButton;

    private Vector3 previousPosition;
    private Vector3 previousAngularPosition;

    private Animator handAnimator;

    public UnityEvent OnGripDown;
    public UnityEvent OnGripUp;
    public UnityEvent OnTriggerDown;
    public UnityEvent OnTriggerUpdating;
    public UnityEvent OnTriggerUp;
    public UnityEvent ThumbstickButtonDown;
    public UnityEvent ThumbstickButtonUpdating;
    public UnityEvent ThumbstickButtonUp;

    void Start()
    {
        handAnimator = GetComponentInChildren<Animator>();

        gripAxis = $"XRI_{hand}_Grip";
        gripButton = $"XRI_{hand}_GripButton";
        triggerAxis = $"XRI_{hand}_Trigger";
        triggerButton = $"XRI_{hand}_TriggerButton";
        thumbstickX = $"XRI_{hand}_Primary2DAxis_Horizontal";
        thumbstickY = $"XRI_{hand}_Primary2DAxis_Vertical";
        thumbstickButton = $"XRI_{hand}_Primary2DAxisClick";
    }

    
    void Update()
    {
        gripValue = Input.GetAxis(gripAxis);
        thumbstick = new Vector2(Input.GetAxis(thumbstickX), Input.GetAxis(thumbstickY));

        // controller velocity using the distance the controller travels
        // between frames
        velocity = (this.transform.position - previousPosition) / Time.deltaTime;
        previousPosition = this.transform.position;
        // controller angular velocity using the distance the controller travels
        // between frames
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
        if (Input.GetButtonDown(triggerButton))
        {
            OnTriggerDown?.Invoke();
        }
        if (Input.GetButtonUp(triggerButton))
        {
            OnTriggerUp?.Invoke();
        }
        if (Input.GetButton(triggerButton))
        {
            OnTriggerUpdating?.Invoke();
        }
        if (Input.GetButtonDown(thumbstickButton))
        {
            ThumbstickButtonDown?.Invoke();
        }
        if (Input.GetButtonUp(thumbstickButton))
        {
            ThumbstickButtonUp?.Invoke();
        }
        if (Input.GetButton(thumbstickButton))
        {
            ThumbstickButtonUpdating?.Invoke();
            Debug.Log("thumbstickbutton");
        }

    }
}

[System.Serializable]
public enum Hand
{
    Left,
    Right
}