using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI notificationTMP;
    [SerializeField] private const float notificationShowTime = 3.0f;


    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Notification, this);
        UIManager.Instance.hideUI(UIManager.UI.Notification);
    }


    public void showNotification(string message)
    {
        notificationTMP.text = message;
        Invoke(nameof(hideNotification), notificationShowTime);
    }


    private void hideNotification()
    {
        UIManager.Instance.hideUI(UIManager.UI.Notification);
        notificationTMP.text = "";
    }
}
