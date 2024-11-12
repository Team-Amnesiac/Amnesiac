using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private float health;
    [SerializeField] private float meleeDamage;
    [SerializeField] private float exp;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI ExpText;

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseHealth(int value)
    {
        health += value;
        if (HealthText != null)
        {
            Debug.Log($"Updating HealthText: {HealthText.text}");
            HealthText.text = "HP: " + health;
        }
        else
        {
            Debug.LogError("HealthText reference is null!");
        }
    }

    public void IncreaseExp(int value)
    {
        exp += value;
        if (ExpText != null)
        {
            Debug.Log($"Updating ExpText: {ExpText.text}");
            ExpText.text = "EXP: "+ exp;
        }
        else
        {
            Debug.LogError("ExpText reference is null!");
        }
    }
}
