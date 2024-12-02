using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

// This class controls the behavior of an inventory item in the UI.
// It manages the item's display, usage, removal, and equipped state,
// as well as updates UI elements like item names, icons, and equipped indicators.
public class InventoryItemController : MonoBehaviour
{
    // Reference to the UI text element displaying the item's name.
    [SerializeField] private TextMeshProUGUI itemNameTMP;
    // Reference to the button used to interact with the item.
    [SerializeField] private Button          itemButton;
    // Reference to the button used to remove the item from the inventory.
    [SerializeField] private Button          removeItemButton;
    // Reference to the image displayed when the item is equipped.
    [SerializeField] private Image           equippedImage;

    // Holds the item's scriptable object data.
    private ItemSO item;

    // Initializes the button listeners for using and removing items.
    void Start()
    {
        itemButton.onClick.AddListener(useItem); // Attach the `useItem` method to the itemButton's click event.
        removeItemButton.onClick.AddListener(removeItem); // Attach the `removeItem` method to the removeItemButton's click event.
    }

    // Removes the item from the inventory and destroys its UI representation.
    private void removeItem()
    {
        InventoryManager.Instance.removeItem(item); // Remove the item from the inventory system.
        Destroy(gameObject); // Destroy the UI element representing this inventory item.
    }

    // Handles the logic for using an inventory item.
    public void useItem()
    {
        item.use(); // Call the item's use method.

        if (item.getItemType() == ItemSO.ItemType.SkillCard)  // Check if the item is a skill card.
        {
            SkillCardSO skillCard = (SkillCardSO)item; // Cast the item to a SkillCardSO.
            if (skillCard.isEquipped())  // Check if the skill card is equipped.
            {
                equipItem(); // Mark the item as equipped visually.
            }
            else                         // If the skill card is not equipped.
            {
                unequipItem(); // Mark the item as unequipped visually.
            }
        }

        if (GameManager.Instance.getGameState() == GameManager.GameState.Battle)
        {
            UIManager.Instance.addCombatLogMessage($"Player used {item.getItemName()}"); // Add a combat log message.
            UIManager.Instance.updateUI(UIManager.UI.Battle); // Update the battle UI.
            BattleManager.Instance.swapTurns(); // Switch turns in the battle system.
        }

        if (item.getItemType() != ItemSO.ItemType.SkillCard)  // If the item is not a skill card.
        {
            removeItem(); // Remove the item from the inventory after use.
        }
    }

    // Marks the item as equipped by displaying the equipped indicator.
    public void equipItem()
    {
        equippedImage.gameObject.SetActive(true); // Activate the equippedImage GameObject.
    }

    // Marks the item as unequipped by hiding the equipped indicator.
    public void unequipItem()
    {
        equippedImage.gameObject.SetActive(false); // Deactivate the equippedImage GameObject.
    }

    // Returns the item associated with this inventory entry.
    public ItemSO getItem()
    {
        return item; // Return the item's scriptable object.
    }

    // Sets the item for this inventory entry and updates its UI elements.
    public void setItem(ItemSO item)
    {
        this.item = item;                                // Set the item.
        itemNameTMP.text = item.getItemName();           // Set the item name text.
        itemButton.image.sprite = item.getItemSprite();  // Set the inventory item sprite.

        if (item.getItemType() == ItemSO.ItemType.SkillCard)  // If the item is a skill card.
        {
            removeItemButton.gameObject.SetActive(false); // Disable the remove button for skill cards.

            SkillCardSO skillCard = (SkillCardSO)item; // Cast the item to a SkillCardSO.
            if (skillCard.isEquipped())  // Check if the skill card is equipped.
            {
                equipItem(); // Display the equipped item indicator.
                return;
            }
        }
        unequipItem(); // Hide the equipped item indicator if the item is not equipped.
    }

    // Sets whether the item can be removed from the inventory.
    public void setRemovable(bool removable)
    {
        if (item.getItemType() == ItemSO.ItemType.SkillCard) return;  // Prevent removal of skill cards.

        removeItemButton.gameObject.SetActive(removable); // Enable or disable the remove button based on `removable`.
    }
}
