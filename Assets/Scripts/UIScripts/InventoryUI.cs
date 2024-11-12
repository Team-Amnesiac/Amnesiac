using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject persistentImageWithText;

    void Start()
    {
        if (inventoryPanel == null)
        {
            Debug.LogError("Inventory Panel is not assigned!");
        }

        if (persistentImageWithText == null)
        {
            Debug.LogError("Persistent Image with Text is not assigned!");
        }

        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }

        if (persistentImageWithText != null)
        {
            persistentImageWithText.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (inventoryPanel != null && persistentImageWithText != null)
        {
            bool isInventoryActive = inventoryPanel.activeSelf;
            InventoryManager.Instance.ListItems();
            inventoryPanel.SetActive(!isInventoryActive);

            Transform imageChild = persistentImageWithText.transform.Find("Image");
            Transform textChild = persistentImageWithText.transform.Find("Text");

            if (imageChild != null)
                imageChild.gameObject.SetActive(isInventoryActive);

            if (textChild != null)
                textChild.gameObject.SetActive(isInventoryActive);
        }
    }

    public void CloseInventory()
    {
        if (inventoryPanel != null && persistentImageWithText != null)
        {
            inventoryPanel.SetActive(false);
            persistentImageWithText.SetActive(true);
        }
    }
}
