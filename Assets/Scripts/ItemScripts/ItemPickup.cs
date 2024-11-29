using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPickup : MonoBehaviour
{
    public ItemSO    item;
    private bool     playerNearby = false;
    private Animator playerAnimator;


    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed, attempting to pick up item.");
            pickup();
        }
    }


    void pickup()
    {
        if (playerAnimator != null)
        {
            Debug.Log("Triggering gather animation.");
            playerAnimator.SetTrigger("Gather");
        }

        Debug.Log($"Picking up item: {item.name}");
        ItemSO.ItemType itemType = item.getItemType();
        if (itemType == ItemSO.ItemType.Relic)
        {
            QuestManager.Instance.addRelic((RelicSO)item);
        }
        else if (itemType == ItemSO.ItemType.Collectible)  
        {
            CollectibleManager.Instance.addCollectible((CollectibleSO)item);
            QuestManager.Instance.addCollectible((CollectibleSO)item);
        }
        else  // Item is not a collectible or a relic.
        {
             InventoryManager.Instance.addItem(item);
        }
        
        Destroy(gameObject);
    }


    public void setPlayerNearby(bool playerNearby)
    {
        this.playerNearby = playerNearby;
    }


    public void setPlayerAnimator(Animator playerAnimator)
    {
        this.playerAnimator = playerAnimator;
    }
}
