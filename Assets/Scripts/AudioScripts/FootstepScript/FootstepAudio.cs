using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the audio for the player's footsteps
public class FootstepAudio : MonoBehaviour
{
    // Reference to the AudioSource that is responsible for playing footstep sounds
    public AudioSource footstepSound;

    // This is called once per frame.
    void Update()
    {
        // Check if any movement keys are being pressed
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            // play the footstep sound if a movement key is pressed
            footstepSound.enabled = true;
        }
        else
        {
            // Disable the footstep sound if no movement keys are pressed
            footstepSound.enabled = false;
        }
    }
}

