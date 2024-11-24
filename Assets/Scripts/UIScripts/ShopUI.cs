using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private GameObject shopContent;
    [SerializeField] private TextMeshProUGUI totalCostText;
    [SerializeField] private TextMeshProUGUI playerCurrencyText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button exitShopButton;


    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Shop, this);
        UIManager.Instance.hideUI(UIManager.UI.Shop);
        buyButton.onClick.AddListener(buyButtonOnClick);
        exitShopButton.onClick.AddListener(exitShopButtonOnClick);
    }


    public void prepareShopShow()
    {
        foreach (ItemSO item in ShopManager.Instance.getShopItems())
        {
            GameObject shopItem = Instantiate(shopItemPrefab, shopContent.transform);

            ShopItemController itemController = shopItem.GetComponent<ShopItemController>();

            // Set up the shop item UI using the ScriptableObject data
            itemController.initialize(item);
        }
        updateVisuals();
    }


    public void prepareShopHide()
    {
        foreach (Transform child in shopContent.transform)
        {
            Destroy(child.gameObject);
        }
    }


    public void updateVisuals()
    {
        totalCostText.text = $"Total Cost: {ShopManager.Instance.getTotalCost()}";
        playerCurrencyText.text = $"Currency: {PlayerManager.Instance.getCurrency()}";
    }


    private void buyButtonOnClick()
    {
        if (PlayerManager.Instance.getCurrency() < ShopManager.Instance.getTotalCost())
        {
            UIManager.Instance.newNotification("Not enough currency for this purchase!");
            Debug.Log("Not enough currency!");
        }
        else
        {
            ShopManager.Instance.finalizePurchase();
        }
    }


    private void exitShopButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.Shop);
        UIManager.Instance.showUI(UIManager.UI.Keeper);
    }
}
