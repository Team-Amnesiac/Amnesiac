using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    [SerializeField] private TextMeshProUGUI totalCostText;
    [SerializeField] private TextMeshProUGUI playerCurrencyText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button exitShopButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Shop, this);
        UIManager.Instance.hideUI(UIManager.UI.Shop);
        buyButton.onClick.AddListener(() =>
        {
                ShopManager.Instance.FinalizePurchase();
        });
        exitShopButton.onClick.AddListener(exitShopButtonOnClick);

        UpdatePlayerCurrency(PlayerManager.Instance.GetCurrency());
        UpdateTotalCost(0);
    }

    ///
    /// Updates the total cost display in the shop UI.
    ///
    public void UpdateTotalCost(int totalCost)
    {
        totalCostText.text = $"Total Cost: {totalCost}";
    }

    ///
    /// Updates the player's currency display in the shop UI.
    ///
    public void UpdatePlayerCurrency(int currency)
    {
        playerCurrencyText.text = $"Currency: {currency}";
    }

    ///
    /// Displays a warning if the player doesn't have enough currency, could have an additional UI element
    ///
    public void ShowNotEnoughCurrencyWarning()
    {
        Debug.Log("Not enough currency!"); // Placeholder for an actual UI warning
        // can add a UI popup or highlight the issue visually here if needed, but not necessary.
    }


    private void exitShopButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.Shop);
        UIManager.Instance.showUI(UIManager.UI.Keeper);
    }
}
