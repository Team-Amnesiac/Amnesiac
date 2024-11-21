using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Shop, this);
        UIManager.Instance.hideUI(UIManager.UI.Shop);
    }


    public void show()
    {
        gameObject.SetActive(true);
    }


    public void hide()
    {
        gameObject.SetActive(false);
    }
}
