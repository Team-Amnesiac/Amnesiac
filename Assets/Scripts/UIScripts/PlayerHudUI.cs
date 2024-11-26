using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHudUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerHealthTMP;
    [SerializeField]private TextMeshProUGUI playerLevelTMP;
    

    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.PlayerHud, this);
        updateVisuals();
        PlayerManager.Instance.OnLevelUp += UpdateLevelUI;
    }

    void OnDestory()
    {
        PlayerManager.Instance.OnLevelUp -= UpdateLevelUI;
    }

    public void updateVisuals()
    {
        playerHealthTMP.text = $"HP: {PlayerManager.Instance.getHealth()}";
        playerLevelTMP.text = $"Level: {PlayerManager.Instance.getPlayerLevel()}";
    }

    private void UpdateLevelUI(int newLevel)
    {
        playerLevelTMP.text = $"Level: {newLevel}";
        updateVisuals();
    }
}
