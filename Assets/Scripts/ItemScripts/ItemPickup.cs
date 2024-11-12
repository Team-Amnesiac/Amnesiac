using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    private bool isPlayerNearby = false;
    private Animator playerAnimator;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter called by: {other.name}");

        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            playerAnimator = other.GetComponent<Animator>();
            Debug.Log("Player is near the item.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"OnTriggerExit called by: {other.name}");

        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            playerAnimator = null;
            Debug.Log("Player left the item.");
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed, attempting to pick up item.");
            Pickup();
        }
    }

    void Pickup()
    {
        if (playerAnimator != null)
        {
            Debug.Log("Triggering gather animation.");
            playerAnimator.SetTrigger("Gather");
        }

        Debug.Log($"Picking up item: {Item.name}");
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }
}
