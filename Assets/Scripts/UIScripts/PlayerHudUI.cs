using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHudUI : MonoBehaviour
{
    [SerializeField] private Image playerHealthFillBar;
    [SerializeField] private Image playerStaminaFillBar;
    [SerializeField] private TextMeshProUGUI playerLevelTMP;
    

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
        playerHealthFillBar.fillAmount  = PlayerManager.Instance.calculateHealthPercent();
        playerStaminaFillBar.fillAmount = PlayerManager.Instance.getStamina() / 100.0f;
        playerLevelTMP.text = $"Level: {PlayerManager.Instance.getPlayerLevel()}";
    }

    private void UpdateLevelUI(int newLevel)
    {
        playerLevelTMP.text = $"Level: {newLevel}";
        updateVisuals();
    }
}
