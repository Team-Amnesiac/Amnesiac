using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemSO    item;
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

        Debug.Log($"Picking up item: {item.name}");
        ItemSO.ItemType itemType = item.GetItemType();
        if (itemType != ItemSO.ItemType.Collectible &&
            itemType != ItemSO.ItemType.Relic)  // Item is not a collectible or a relic.
        {
            InventoryManager.Instance.Add(item);
        }
        else if (itemType == ItemSO.ItemType.Relic)
        {
            QuestManager.Instance.addRelic((RelicSO)item);
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
