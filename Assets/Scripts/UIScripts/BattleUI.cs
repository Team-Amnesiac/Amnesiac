using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnIndicatorTMP;
    [SerializeField] private TextMeshProUGUI roundTMP;
    [SerializeField] private TextMeshProUGUI playerTurnTMP;
    [SerializeField] private TextMeshProUGUI enemyTurnTMP;
    [SerializeField] private TextMeshProUGUI criticalHitTMP;
    [SerializeField] private TextMeshProUGUI skillCard1TMP;
    [SerializeField] private TextMeshProUGUI skillCard2TMP;
    [SerializeField] private TextMeshProUGUI skillCard3TMP;
    [SerializeField] private TextMeshProUGUI skillCard4TMP;

    [SerializeField] private Button meleeAttackButton;
    [SerializeField] private Button skillCardButton1;
    [SerializeField] private Button skillCardButton2;
    [SerializeField] private Button skillCardButton3;
    [SerializeField] private Button skillCardButton4;
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Slider enemyHealthSlider;


    /* UNITY FUNCTIONS */

    void Start()
    {
        turnIndicatorTMP.gameObject.SetActive(true);
        criticalHitTMP.gameObject.SetActive(false);
        
        if (PlayerManager.Instance.GetSkillCard(PlayerManager.SkillSlot.One) == null)
        {
            skillCard1TMP.text = "EMPTY";
            skillCardButton1.interactable = false;
        }
        else
        {
            SkillCard skillCard1 = PlayerManager.Instance.GetSkillCard(PlayerManager.SkillSlot.One);
            skillCard1TMP.text = $"{skillCard1.itemName}";
        }
        if (PlayerManager.Instance.GetSkillCard(PlayerManager.SkillSlot.Two) == null)
        {
            skillCard2TMP.text = "EMPTY";
            skillCardButton2.interactable = false;
        }
        else
        {
            SkillCard skillCard2 = PlayerManager.Instance.GetSkillCard(PlayerManager.SkillSlot.Two);
            skillCard2TMP.text = $"{skillCard2.itemName}";
        }
        if (PlayerManager.Instance.GetSkillCard(PlayerManager.SkillSlot.Three) == null)
        {
            skillCard3TMP.text = "EMPTY";
            skillCardButton3.interactable = false;
        }
        else
        {
            SkillCard skillCard3 = PlayerManager.Instance.GetSkillCard(PlayerManager.SkillSlot.Three);
            skillCard3TMP.text = $"{skillCard3.itemName}";
        }
        if (PlayerManager.Instance.GetSkillCard(PlayerManager.SkillSlot.Four) == null)
        {
            skillCard4TMP.text = "EMPTY";
            skillCardButton4.interactable = false;
        }
        else
        {
            SkillCard skillCard4 = PlayerManager.Instance.GetSkillCard(PlayerManager.SkillSlot.Four);
            skillCard4TMP.text = $"{skillCard4.itemName}";
        }

        meleeAttackButton.onClick.AddListener(OnMeleeAttackButtonClicked);
        skillCardButton1.onClick.AddListener(OnSkillCardButton1Clicked);
        skillCardButton2.onClick.AddListener(OnSkillCardButton2Clicked);
        skillCardButton3.onClick.AddListener(OnSkillCardButton3Clicked);
        skillCardButton4.onClick.AddListener(OnSkillCardButton4Clicked);

        // Start the battle and pass this object to the BattleManager.
        BattleManager.Instance.StartBattle(this);
    }
    

    void OnDestroy()
    {
        meleeAttackButton.onClick.RemoveListener(OnMeleeAttackButtonClicked);
        skillCardButton1.onClick.RemoveListener(OnSkillCardButton1Clicked);
        skillCardButton2.onClick.RemoveListener(OnSkillCardButton2Clicked);
        skillCardButton3.onClick.RemoveListener(OnSkillCardButton3Clicked);
        skillCardButton4.onClick.RemoveListener(OnSkillCardButton4Clicked);
    }


    /* PUBLIC FUNCTIONS */

    public void ShowCriticalHit()
    {
        StartCoroutine(CriticalHitCoroutine());
    }

    public void UpdateHealthBars()
    {
        UpdatePlayerHealthBar();
        UpdateEnemyHealthBar();
    }


    public void UpdateRoundCounter()
    {
        roundTMP.text = "Round: " + BattleManager.Instance.GetRound() / 2;
    }


    public void SetButtonInteractability(bool interactable)
    {
        meleeAttackButton.interactable = interactable;
        if (PlayerManager.Instance.GetSkillCard(PlayerManager.SkillSlot.One) != null)
        {
            skillCardButton1.interactable = interactable;
        }
        if (PlayerManager.Instance.GetSkillCard(PlayerManager.SkillSlot.Two) != null)
        {
            skillCardButton2.interactable = interactable;
        }
        if (PlayerManager.Instance.GetSkillCard(PlayerManager.SkillSlot.Three) != null)
        {
            skillCardButton3.interactable = interactable;
        }
        if (PlayerManager.Instance.GetSkillCard(PlayerManager.SkillSlot.Four) != null)
        {
            skillCardButton4.interactable = interactable;
        }
    }


    public void UpdateTurnText(BattleManager.Attacker attacker)
    {
        if (attacker == BattleManager.Attacker.Player)  // Player's turn.
        {
            turnIndicatorTMP.text = "Player's Turn";
        }
        else                                            // Enemy's turn.
        {
            turnIndicatorTMP.text = "Enemy's Turn";
        }
    }


    /* BUTTON ON CLICK LISTENERS */

    private void OnMeleeAttackButtonClicked()
    {
        Debug.Log("Player uses Melee attack.");
        BattleManager.Instance.PlayerAttack(PlayerManager.SkillSlot.Melee);
    }


    private void OnSkillCardButton1Clicked()
    {
        Debug.Log("Player uses Skill Card 1.");
        BattleManager.Instance.PlayerAttack(PlayerManager.SkillSlot.One);
    }

    private void OnSkillCardButton2Clicked()
    {
        Debug.Log("Player uses Skill Card 2.");
        BattleManager.Instance.PlayerAttack(PlayerManager.SkillSlot.Two);
    }


    private void OnSkillCardButton3Clicked()
    {
        Debug.Log("Player uses Skill Card 3.");
        BattleManager.Instance.PlayerAttack(PlayerManager.SkillSlot.Three);
    }


    private void OnSkillCardButton4Clicked()
    {
        Debug.Log("Player uses Skill Card 4.");
        BattleManager.Instance.PlayerAttack(PlayerManager.SkillSlot.Four);
    }


    /* PRIVATE FUNCTIONS */


    private IEnumerator CriticalHitCoroutine()
    {
        criticalHitTMP.gameObject.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        criticalHitTMP.gameObject.SetActive(false);
    }


    private void UpdatePlayerTurnText()
    {
        playerTurnTMP.text = "Turn: " + BattleManager.Instance.GetPlayerTurnCount();
    }


    private void UpdateEnemyTurnText()
    {
        enemyTurnTMP.text = "Turn: " + BattleManager.Instance.GetEnemyTurnCount();
    }


    private void UpdatePlayerHealthBar()
    {
        // Set the player health bar slider to their health percentage from 1.0f to 0.0f.
        playerHealthSlider.value = PlayerManager.Instance.CalculateHealthPercent();
    }


    private void UpdateEnemyHealthBar()
    {
        // Set the enemy health bar slider to their health percentage from 0.0f to 1.0f.
        enemyHealthSlider.value = Mathf.Abs(BattleManager.Instance.GetEnemyHealthPercentage() - 1.0f);
    }
}
