// This class manages the Keeper UI, allowing players to interact with The Keeper for dialogue, shop, and collectibles.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeeperUI : MonoBehaviour
{
    // Buttons for interacting with the Keeper's options.
    [SerializeField] private Button exitButton;          // Button to exit the Keeper UI.
    [SerializeField] private Button talkButton;          // Button to initiate dialogue with The Keeper.
    [SerializeField] private Button shopButton;          // Button to open the shop.
    [SerializeField] private Button collectiblesButton;  // Button to view collectibles.
    // Reference to the KeeperInteraction script for handling dialogue logic.
    [SerializeField] private KeeperInteraction keeperInteraction;

    // Initializes the Keeper UI and attaches button listeners.
    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Keeper, this); // Register the Keeper UI with the UIManager.
        UIManager.Instance.hideUI(UIManager.UI.Keeper); // Hide the Keeper UI initially.

        // Attach listeners to button click events.
        exitButton.onClick.AddListener(exitButtonOnClick);
        talkButton.onClick.AddListener(talkButtonOnClick);
        shopButton.onClick.AddListener(shopButtonOnClick);
        collectiblesButton.onClick.AddListener(collectiblesButtonOnClick);
    }

    // Handles the exit button click, resuming the game and hiding the Keeper UI.
    private void exitButtonOnClick()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Play); // Resume the game state to "Play."
        UIManager.Instance.hideUI(UIManager.UI.Keeper); // Hide the Keeper UI.
    }

    // Handles the talk button click, initiating dialogue with The Keeper.
    private void talkButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.Keeper); // Hide the Keeper UI.
        UIManager.Instance.showUI(UIManager.UI.Dialogue); // Show the Dialogue UI.
        keeperInteraction.talkTo(); // Trigger the Keeper's dialogue logic.
    }

    // Handles the shop button click, opening the shop UI.
    private void shopButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.Keeper); // Hide the Keeper UI.
        UIManager.Instance.showUI(UIManager.UI.Shop); // Show the Shop UI.
    }

    // Handles the collectibles button click, opening the collectibles UI.
    private void collectiblesButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.Keeper); // Hide the Keeper UI.
        UIManager.Instance.showUI(UIManager.UI.Collectibles); // Show the Collectibles UI.
    }
}
