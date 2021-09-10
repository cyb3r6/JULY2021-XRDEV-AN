using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    /// <summary>
    /// The number of triggers to open the door
    /// </summary>
    public int maxIndex;

    public int doorIndex;

    private Animator doorAnimator;

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddTrigger();
        }
    }

    public void AddTrigger()
    {
        doorIndex++;

        if(doorIndex >= maxIndex)
        {
            OpenDoor(); 
        }
    }

    private void OpenDoor()
    {
        doorAnimator.SetTrigger("Open");
    }
}
