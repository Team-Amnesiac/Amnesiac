// This class manages the Prompt UI, which temporarily displays messages to the player for interaction or guidance.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptUI : MonoBehaviour
{
    // The UI element for displaying the prompt message.
    [SerializeField] private GameObject promptUI;
    // Text component for displaying the prompt message.
    [SerializeField] private TextMeshProUGUI promptTMP;
    // Duration for which the prompt will be shown before hiding.
    [SerializeField] private const float promptShowTime = 5.0f;

    // Initializes the Prompt UI and hides it by default.
    private void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Prompt, this); // Register the Prompt UI with the UIManager.
        UIManager.Instance.hideUI(UIManager.UI.Prompt); // Hide the Prompt UI initially.
    }

    // Displays a prompt message for a limited time.
    public void showPrompt(string message)
    {
        promptTMP.text = message; // Set the prompt text to the provided message.
        UIManager.Instance.showUI(UIManager.UI.Prompt); // Show the Prompt UI.
        Invoke(nameof(hidePrompt), promptShowTime); // Schedule the prompt to hide after the specified time.
    }

    // Hides the prompt and clears the message.
    private void hidePrompt()
    {
        UIManager.Instance.hideUI(UIManager.UI.Prompt); // Hide the Prompt UI.
        promptTMP.text = ""; // Clear the prompt text.
    }
}
