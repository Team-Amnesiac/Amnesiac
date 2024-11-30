using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubFootstepAudio : MonoBehaviour
{
    public AudioSource footstepSound;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            footstepSound.enabled = true;
        }
        else
        {
            footstepSound.enabled = false;
        }
    }
}
