using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationUI : MonoBehaviour
{
    private Coroutine activeCoroutine;
    [SerializeField] private TextMeshProUGUI notificationTMP;
    [SerializeField] private const float notificationShowTime = 3.0f;

    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Notification, this);
        UIManager.Instance.hideUI(UIManager.UI.Notification);

        //subscribing to the OnLevelUp event from PlayerManager
        PlayerManager.Instance.OnLevelUp += showLevelUpNotification;
    }

    void OnDestroy()
    {
        //unsubscribing would avoid memory leaks
        PlayerManager.Instance.OnLevelUp -= showLevelUpNotification;
    }

    private void showLevelUpNotification(int newLevel)
    {
        showNotification($"You leveled up! New Level: {newLevel}");
    }

    public void showNotification(string message)
    {
        // to stop any currently running coroutine to avoid overlapping notifications (e.g- New Quest notification and level up notif overlap)
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }

        notificationTMP.text = message;
        UIManager.Instance.showUI(UIManager.UI.Notification);

        // start the coroutine to hide the notification after the delay
        activeCoroutine = StartCoroutine(HideNotificationAfterDelay());
    }

    private IEnumerator HideNotificationAfterDelay()
    {
        yield return new WaitForSeconds(notificationShowTime);
        UIManager.Instance.hideUI(UIManager.UI.Notification);
        notificationTMP.text = "";
    }
}
