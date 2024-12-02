// This class manages the Dialogue UI, displaying dialogue messages and handling user input to progress the dialogue.

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component for displaying dialogue text.
    [SerializeField] private TextMeshProUGUI dialogueTMP;

    // Event triggered when the player progresses to the next dialogue.
    public event EventHandler onNextDialogue;

    // Initializes the Dialogue UI and hides it by default.
    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Dialogue, this); // Register the Dialogue UI with the UIManager.
        UIManager.Instance.hideUI(UIManager.UI.Dialogue); // Hide the Dialogue UI initially.
    }

    // Detects user input to progress the dialogue.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) // Check if the right mouse button is clicked.
        {
            onNextDialogue?.Invoke(this, EventArgs.Empty); // Trigger the onNextDialogue event if there are subscribers.
        }
    }

    // Updates the dialogue UI with a new dialogue message.
    public void newDialogue(string message)
    {
        dialogueTMP.text = message; // Set the dialogue text to the new message.
    }
}
