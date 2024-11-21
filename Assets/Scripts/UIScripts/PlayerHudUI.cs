using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHudUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerHealthTMP;
    [SerializeField]private TextMeshProUGUI playerExperienceTMP;
    

    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.PlayerHud, this);
    }


    public void updateVisuals()
    {
        playerHealthTMP.text = $"HP: {PlayerManager.Instance.getHealth()}";
        playerExperienceTMP.text = $"EXP: {PlayerManager.Instance.getExperience()}";
    }
}
