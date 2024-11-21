using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private Toggle     enableRemoveToggle;
    [SerializeField] private GameObject inventoryContent;
    [SerializeField] private Button     closeInventoryButton;


    void Start()
    {
        enableRemoveToggle.onValueChanged.AddListener(enableRemoveToggleOnValueChanged);
        closeInventoryButton.onClick.AddListener(closeInventoryButtonOnClick);
        UIManager.Instance.setUI(UIManager.UI.Inventory, this);
        UIManager.Instance.hideUI(UIManager.UI.Inventory);
    }


    public void prepareInventoryShow()
    {
        foreach (Item item in InventoryManager.Instance.getInventoryItems())
        {

            GameObject obj = Instantiate(inventoryItemPrefab, inventoryContent.transform);

            InventoryItemController controller = obj.GetComponent<InventoryItemController>();
            controller.setItem(item);
            controller.setItemName(item.itemName);
            controller.setSprite(item.sprite);
            if (item.itemType == Item.ItemType.SkillCard || !enableRemoveToggle.isOn)  // Item is a SkillCard or all items cannot be removed.
            {
                controller.setRemovable(false);
            }
            else
            {
                controller.setRemovable(true);
            }
        }
    }


    public void prepareInventoryHide()
    {
        foreach (Transform child in inventoryContent.transform)
        {
            Destroy(child.gameObject);
        }
    }


    private void enableRemoveToggleOnValueChanged(bool canRemove)
    {
        foreach (InventoryItemController controller in
                 inventoryContent.GetComponentsInChildren<InventoryItemController>())
        {
            if (controller.getItem().itemType == Item.ItemType.SkillCard)  // Item is a skill card or a collectible.
            {
                continue;  // Do nothing.
            }

            controller.setRemovable(canRemove);
        }
    }


    private void closeInventoryButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.Inventory);
    }
}
