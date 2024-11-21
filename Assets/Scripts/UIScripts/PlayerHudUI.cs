using System.Collections;
using System.Collections.Generic;
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


    public void show()
    {
        gameObject.SetActive(true);
    }


    public void hide()
    {
        gameObject.SetActive(false);
    }


    private void hideNotification()
    {
        notificationImage.gameObject.SetActive(false);
    }
}
