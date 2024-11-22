using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    public GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(Player);

        if (other.gameObject == Player)
        {
            Debug.Log(Player + "Player entered the trigger zone.");
            Player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Debug.Log(Player + "Player exited the trigger zone.");
            Player.transform.parent = null;
        }
    }

}
