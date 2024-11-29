using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubFootstepAudio : MonoBehaviour
{
    public AudioSource footstepSound;
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.S))
        {
            footstepSound.enabled = true;
        }
        else { footstepSound.enabled = false;}
        
    }
}
