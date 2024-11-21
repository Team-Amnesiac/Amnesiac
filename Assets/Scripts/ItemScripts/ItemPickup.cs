using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item      Item;
    private bool     playerNearby = false;
    private Animator playerAnimator;


    private void Start()
    {
        GetComponentInChildren<ItemVisuals>().SetItemPickup(this);
    }


    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
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
        if (Item.itemType != Item.ItemType.Collectible)  // Item is not a collectible.
        {
            InventoryManager.Instance.Add(Item);
        }
        
        Destroy(gameObject);
    }


    public void SetPlayerNearby(bool playerNearby)
    {
        this.playerNearby = playerNearby;
    }


    public void SetPlayerAnimator(Animator playerAnimator)
    {
        this.playerAnimator = playerAnimator;
    }
}
