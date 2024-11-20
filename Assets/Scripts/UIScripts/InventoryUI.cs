using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private Toggle     enableRemoveToggle;
    [SerializeField] private GameObject inventoryContent;
    [SerializeField] private Button     closeInventoryButton;

    private bool showInventory = false;


    void Awake()
    {
        Debug.Log("InventoryUI is awake!");
    }


    void Start()
    {
        // Pass this InventoryUI to the InventoryManager for communication.
        InventoryManager.Instance.SetInventoryUI(this);
        enableRemoveToggle.onValueChanged.AddListener(enableRemoveToggleOnValueChanged);
        closeInventoryButton.onClick.AddListener(closeInventoryButtonOnClick);
        CloseInventory();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }


    private void enableRemoveToggleOnValueChanged(bool canRemove)
    {
        foreach (InventoryItemController controller in
                 inventoryContent.GetComponentsInChildren<InventoryItemController>())
        {
            if (controller.getItem().itemType == Item.ItemType.SkillCard ||
                controller.getItem().itemType == Item.ItemType.Collectible)  // Item is a skill card or a collectible.
            {
                continue;  // Do nothing.
            }

            controller.setRemovable(canRemove);
        }
    }


    private void closeInventoryButtonOnClick()
    {
        CloseInventory();
        showInventory = false;
    }


    private void ToggleInventory()
    {
        if (showInventory)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }

        // Toggle boolean representing whether inventory should be shown or not.
        showInventory = !showInventory;
    }


    private void OpenInventory()
    {
        InventoryManager.Instance.ListItems();
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }


    private void CloseInventory()
    {
        foreach (Transform child in inventoryContent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }


    public GameObject GetInventoryContent()
    {
        return inventoryContent;
    }
}
