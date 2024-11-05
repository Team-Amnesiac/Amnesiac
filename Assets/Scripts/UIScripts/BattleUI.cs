using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class BattleUI : MonoBehaviour
{
    /* EVENTS */

    // Melee used event.
    public event EventHandler OnMelee;

    // Skill card used event args.
    public class OnSkillCardEventArgs : EventArgs
    {
        public PlayerManager.SkillCardSlot skillCardSlot;
    }
    // Skill card used event.
    public event EventHandler<OnSkillCardEventArgs> OnSkillCard;
    
    

    [SerializeField] private TextMeshProUGUI turnIndicatorTMP;
    [SerializeField] private TextMeshProUGUI roundTMP;
    [SerializeField] private TextMeshProUGUI playerTurnTMP;
    [SerializeField] private TextMeshProUGUI enemyTurnTMP;

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
        // Attach the OnAttackerChange event listener.
        BattleManager.Instance.OnAttackerChange += BattleManagerOnAttackerChange;
        BattleManager.Instance.OnHealthChange += BattleManagerOnHealthChange;

        meleeAttackButton.onClick.AddListener(OnMeleeAttackButtonClicked);
        skillCardButton1.onClick.AddListener(OnSkillCardButton1Clicked);
        skillCardButton2.onClick.AddListener(OnSkillCardButton2Clicked);
        skillCardButton3.onClick.AddListener(OnSkillCardButton3Clicked);
        skillCardButton4.onClick.AddListener(OnSkillCardButton4Clicked);

        BattleManager.Instance.StartBattle();
    }
    

    void OnDestroy()
    {
        // Detach the event listeners.
        BattleManager.Instance.OnAttackerChange -= BattleManagerOnAttackerChange;

        meleeAttackButton.onClick.RemoveListener(OnMeleeAttackButtonClicked);
        skillCardButton1.onClick.RemoveListener(OnSkillCardButton1Clicked);
        skillCardButton2.onClick.RemoveListener(OnSkillCardButton2Clicked);
        skillCardButton3.onClick.RemoveListener(OnSkillCardButton3Clicked);
        skillCardButton4.onClick.RemoveListener(OnSkillCardButton4Clicked);
    }


    /* EVENT LISTENERS */

    private void BattleManagerOnAttackerChange(object sender, EventArgs eventArgs)
    {
        if (BattleManager.Instance.GetAttacker() == BattleManager.Attacker.Enemy)
        {
            // Enemy's turn.
            StartCoroutine(ShowTurnIndicator(false));
            SetButtonInteractability(false);
        }
        else
        {
            // Player's turn.
            StartCoroutine(ShowTurnIndicator(true));
            SetButtonInteractability(true);
        }
    }


    private void BattleManagerOnHealthChange(object sender, EventArgs eventArgs)
    {
        UpdatePlayerHealthBar();
        UpdateEnemyHealthBar();
    }


    /* BUTTON ON CLICK LISTENERS */

    public void OnMeleeAttackButtonClicked()
    {
        OnMelee?.Invoke(this, EventArgs.Empty);
    }

    //placeholders for all the skill cards now
    public void OnSkillCardButton1Clicked()
    {
        Debug.Log("Player uses Skill Card 1.");
        OnSkillCardEventArgs args = new OnSkillCardEventArgs { skillCardSlot = PlayerManager.SkillCardSlot.One };
        OnSkillCard?.Invoke(this, args);
    }

    public void OnSkillCardButton2Clicked()
    {
        Debug.Log("Player uses Skill Card 2.");
        OnSkillCardEventArgs args = new OnSkillCardEventArgs
        {
            skillCardSlot = PlayerManager.SkillCardSlot.Two
        };
        OnSkillCard?.Invoke(this, args);
    }

    public void OnSkillCardButton3Clicked()
    {
        Debug.Log("Player uses Skill Card 3.");
        OnSkillCardEventArgs args = new OnSkillCardEventArgs
        {
            skillCardSlot = PlayerManager.SkillCardSlot.Three
        };
        OnSkillCard?.Invoke(this, args);
    }

    public void OnSkillCardButton4Clicked()
    {
        Debug.Log("Player uses Skill Card 4.");
        OnSkillCardEventArgs args = new OnSkillCardEventArgs { skillCardSlot = PlayerManager.SkillCardSlot.Four };
        OnSkillCard?.Invoke(this, args);
    }


    /* PRIVATE FUNCTIONS */

    private IEnumerator ShowTurnIndicator(bool isPlayerTurn)
    {
        turnIndicatorTMP.text = isPlayerTurn ? "Player's Turn" : "Enemy's Turn";
        turnIndicatorTMP.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        turnIndicatorTMP.gameObject.SetActive(false);
    }


    private void SetButtonInteractability(bool interactable)
    {
        meleeAttackButton.interactable = interactable;
        skillCardButton1.interactable  = interactable;
        skillCardButton2.interactable  = interactable;
        skillCardButton3.interactable  = interactable;
        skillCardButton4.interactable  = interactable;
    }


    private void UpdateRoundText()
    {
        roundTMP.text = "Round: " + BattleManager.Instance.GetRound();
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
        // Set the player health bar slider to their health percentage from 0.0f to 1.0f.
        //playerHealthSlider.value = PlayerManager.Instance.CalculateHealthPercent();
    }


    private void UpdateEnemyHealthBar()
    {
        // Get the EnemyAI the player is engaged in battle with.
        EnemyAI enemyAI = BattleManager.Instance.GetEnemyAI();
        // Set the enemy health bar slider to their health percentage from 0.0f to 1.0f.
        //enemyHealthSlider.value = enemyAI.CalculateHealthPercentage();
    }
}
