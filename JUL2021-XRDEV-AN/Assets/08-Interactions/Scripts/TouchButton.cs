using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchButton : MonoBehaviour
{
    public Transform button;
    public Transform downTransform;
    public AudioSource buttonAudioSource;
    public GameObject shower;
    public UnityEvent OnButtonPressed;

    private bool showerEnabled = false;
    private Vector3 originalStartPosition;

    void Start()
    {
        originalStartPosition = button.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            button.position = downTransform.position;
            buttonAudioSource.Play();
            OnButtonPressed?.Invoke();

            showerEnabled = !showerEnabled;
            shower.SetActive(showerEnabled);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            button.position = originalStartPosition;
        }
    }

}
