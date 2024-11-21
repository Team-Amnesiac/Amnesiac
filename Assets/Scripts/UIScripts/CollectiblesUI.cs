using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesUI : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Collectibles, this);
        UIManager.Instance.hideUI(UIManager.UI.Collectibles);
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
