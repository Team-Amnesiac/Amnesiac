using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// This class manages the Player HUD UI, displaying the player's health, stamina, and level.

public class PlayerHudUI : MonoBehaviour
{
    // UI elements for displaying player stats.
    [SerializeField] private Image playerHealthFillBar; // Health bar to show the player's health percentage.
    [SerializeField] private Image playerStaminaFillBar; // Stamina bar to show the player's stamina percentage.
    [SerializeField] private TextMeshProUGUI playerLevelTMP; // Text to display the player's current level.

    // Initializes the Player HUD and registers for level-up events.
    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.PlayerHud, this); // Register the Player HUD with the UIManager.
        updateVisuals(); // Update the visuals initially.
        PlayerManager.Instance.OnLevelUp += UpdateLevelUI; // Subscribe to the level-up event.
    }

    // Cleans up by unsubscribing from events when the object is destroyed.
    void OnDestory()
    {
        PlayerManager.Instance.OnLevelUp -= UpdateLevelUI; // Unsubscribe from the level-up event.
    }

    // Updates all visual elements of the Player HUD.
    public void updateVisuals()
    {
        // Update the health bar fill amount based on the player's current health percentage.
        playerHealthFillBar.fillAmount = PlayerManager.Instance.calculateHealthPercent();

        // Update the stamina bar fill amount based on the player's current stamina.
        playerStaminaFillBar.fillAmount = PlayerManager.Instance.getStamina() / 100.0f;

        // Update the displayed player level.
        playerLevelTMP.text = $"Level: {PlayerManager.Instance.getPlayerLevel()}";
    }

    // Updates the level UI when the player levels up.
    private void UpdateLevelUI(int newLevel)
    {
        playerLevelTMP.text = $"Level: {newLevel}"; // Update the level text.
        updateVisuals(); // Refresh other visuals to reflect any changes.
    }
}
