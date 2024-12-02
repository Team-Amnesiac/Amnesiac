// This class manages the Notification UI, displaying temporary messages to the player.

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationUI : MonoBehaviour
{
    // Text component for displaying the notification message.
    [SerializeField] private TextMeshProUGUI notificationTMP;
    // Duration for which the notification will be displayed.
    [SerializeField] private const float notificationShowTime = 3.0f;

    // Initializes the Notification UI and hides it by default.
    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Notification, this); // Register the Notification UI with the UIManager.
        UIManager.Instance.hideUI(UIManager.UI.Notification); // Hide the Notification UI initially.
    }

    // Displays a notification with the specified message for a limited time.
    public void showNotification(string message)
    {
        notificationTMP.text = message; // Set the notification text.
        UIManager.Instance.showUI(UIManager.UI.Notification); // Show the Notification UI.
        Invoke(nameof(hideNotification), notificationShowTime); // Schedule the notification to hide after the specified time.
    }

    // Hides the notification and clears the message.
    private void hideNotification()
    {
        UIManager.Instance.hideUI(UIManager.UI.Notification); // Hide the Notification UI.
        notificationTMP.text = ""; // Clear the notification text.
    }
}
