using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script attaches the player to a moving platform when they enter a trigger zone, and detaches them when they exit that zone
public class PlatformAttach : MonoBehaviour
{
    // The player GameObject that will be attached to the platform
    public GameObject Player;

    // is called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.gameObject == Player)
        {
            // Set the player's parent to the platform to make the player move with the platform
            Player.transform.parent = transform;
        }
    }

    // this is called when another collider exits the trigger collider attached to this object
    private void OnTriggerExit(Collider other)
    {
        // will check if the object exiting the trigger is the player
        if (other.gameObject == Player)
        {
            // detach the player from the platform
            Player.transform.parent = null;
        }
    }
}
