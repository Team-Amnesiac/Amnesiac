using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;


public class InventoryItemController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameTMP;
    [SerializeField] private Button          itemButton;
    [SerializeField] private Button          removeItemButton;
    [SerializeField] private Image           equippedImage;

    private ItemSO item;


    void Start()
    {
        itemButton.onClick.AddListener(useItem);
        removeItemButton.onClick.AddListener(removeItem);
    }

    private void removeItem()
    {
        InventoryManager.Instance.removeItem(item);
        Destroy(gameObject);
    }


    public void useItem()
    {
        item.use();
        if (item.getItemType() == ItemSO.ItemType.SkillCard)  // Item is a skill card.
        {
            SkillCardSO skillCard = (SkillCardSO)item;
            if (skillCard.isEquipped())  // Skill card was equipped.
            {
                equipItem();
            }
            else                         // Skill card was unequipped.
            {
                unequipItem();
            }
        }
        else
        {
            InventoryManager.Instance.removeItem(item);
        }
    }


    public void equipItem()
    {
        equippedImage.gameObject.SetActive(true);
    }


    public void unequipItem()
    {
        equippedImage.gameObject.SetActive(false);
    }


    public ItemSO getItem()
    {
        return item;
    }


    public void setItem(ItemSO item)
    {
        // Update the inventory item context.
        this.item = item;                                // Set the item.
        itemNameTMP.text = item.getItemName();           // Set the item name text.
        itemButton.image.sprite = item.getItemSprite();  // Set the inventory item sprite.

        if (item.getItemType() == ItemSO.ItemType.SkillCard)  // Item is a skill card.
        {
            // Cannot remove a skill card.
            removeItemButton.gameObject.SetActive(false);

            // Get the skill card script.
            SkillCardSO skillCard = (SkillCardSO)item;
            if (skillCard.isEquipped())  // Skill card is equipped.
            {
                // Display the equipped item indicator.
                equipItem();

                return;
            }
        }
        // Hide the equipped item indicator, as the item is not equipped.
        unequipItem();
    }


    public void setRemovable(bool removable)
    {
        if (item.getItemType() == ItemSO.ItemType.SkillCard) return;  // Cannot remove a skill card.

        removeItemButton.gameObject.SetActive(removable);
    }
}
