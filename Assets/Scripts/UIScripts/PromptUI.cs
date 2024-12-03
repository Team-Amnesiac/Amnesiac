using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PromptUI : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private TextMeshProUGUI promptTMP;
    [SerializeField] private const float promptShowTime = 1.5f;

    // Tracks entities that have already triggered prompts
    private HashSet<GameObject> shownPrompts = new HashSet<GameObject>();

    private void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Prompt, this); // Register the Prompt UI with the UIManager.
        UIManager.Instance.hideUI(UIManager.UI.Prompt); // Hide the Prompt UI initially.
    }

    // Displays a prompt message for a specific entity.
    public void showPrompt(GameObject entity, string message)
    {
        if (shownPrompts.Contains(entity)) return; // Do not show the prompt if it was already displayed for this entity.

        promptTMP.text = message; // Set the prompt text to the provided message.
        UIManager.Instance.showUI(UIManager.UI.Prompt); // Show the Prompt UI.
        shownPrompts.Add(entity); // Mark this entity as shown.
        Invoke(nameof(hidePrompt), promptShowTime); // Schedule the prompt to hide after the specified time.
    }

    // Hides the prompt and clears the message.
    private void hidePrompt()
    {
        UIManager.Instance.hideUI(UIManager.UI.Prompt); // Hide the Prompt UI.
        promptTMP.text = ""; // Clear the prompt text.
    }

    // Resets the "shown" state for an entity (if needed when the player leaves the vicinity).
    public void resetPrompt(GameObject entity)
    {
        if (shownPrompts.Contains(entity))
        {
            shownPrompts.Remove(entity); // Allow the prompt to be shown again for this entity.
        }
    }
}