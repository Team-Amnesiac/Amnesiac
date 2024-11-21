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
        // Pass this InventoryUI to the InventoryManager for communication.
        InventoryManager.Instance.SetInventoryUI(this);
        enableRemoveToggle.onValueChanged.AddListener(enableRemoveToggleOnValueChanged);
        closeInventoryButton.onClick.AddListener(closeInventoryButtonOnClick);
        UIManager.Instance.setUI(UIManager.UI.Inventory, this);
        UIManager.Instance.hideUI(UIManager.UI.Inventory);
    }


    public void prepareInventoryShow()
    {
        InventoryManager.Instance.ListItems();
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


    public GameObject GetInventoryContent()
    {
        return inventoryContent;
    }
}
