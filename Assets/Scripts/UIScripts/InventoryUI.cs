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
        foreach (ItemSO item in InventoryManager.Instance.getInventoryItems())
        {
            GameObject obj = Instantiate(inventoryItemPrefab, inventoryContent.transform);

            InventoryItemController controller = obj.GetComponent<InventoryItemController>();
            controller.setItem(item);
            controller.setItemName(item.getItemName());
            controller.setSprite(item.getItemSprite());
            if (item.getItemType() == ItemSO.ItemType.SkillCard)  // Item is a SkillCard or all items cannot be removed.
            {
                controller.setRemovable(false);
                SkillCardSO skillCard = (SkillCardSO)item;
                if (skillCard.isEquipped())
                {
                    controller.equipItem();
                }
                else
                {
                    controller.unequipItem();
                }
            }
            else if (!enableRemoveToggle.isOn)
            {
                controller.unequipItem();
                controller.setRemovable(false);
            }
            else
            {
                controller.unequipItem();
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
            if (controller.getItem().getItemType() == ItemSO.ItemType.SkillCard)  // Item is a skill card or a collectible.
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
