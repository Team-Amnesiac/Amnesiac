using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This Door script handles the opening and closing of a door using animations when a player enters or exits a trigger area
public class Door : MonoBehaviour
{
    // The Animator components for the right and left doors
    public Animator rightDoorAnimator;
    public Animator leftDoorAnimator;

    // called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter(Collider other)
    {
        // Set the isOpen bool parameter of both animators to true to open the doors.
        rightDoorAnimator.SetBool("isOpen", true);
        leftDoorAnimator.SetBool("isOpen", true);
    }

    // on trigger exit is called when another collider exits the trigger collider attached to this object
    private void OnTriggerExit(Collider other)
    {
        // Set the isOpen bool parameter of both animators to false to close the doors
        rightDoorAnimator.SetBool("isOpen", false);
        leftDoorAnimator.SetBool("isOpen", false);
    }
}
