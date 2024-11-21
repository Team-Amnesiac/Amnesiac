using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHudUI : MonoBehaviour
{
    [SerializeField] private Image notificationImage;


    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.PlayerHud, this);
        hideNotification();
    }


    public void addNotification(string message)
    {
        notificationImage.GetComponent<TextMeshProUGUI>().text = message;
        showNotification();
    }


    private void showNotification()
    {
        notificationImage.gameObject.SetActive(true);
        Invoke(nameof(hideNotification), 3f);
    }


    private void hideNotification()
    {
        notificationImage.gameObject.SetActive(false);
    }
}
