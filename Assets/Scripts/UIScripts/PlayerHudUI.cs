using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHudUI : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.PlayerHud, this);
    }
}
