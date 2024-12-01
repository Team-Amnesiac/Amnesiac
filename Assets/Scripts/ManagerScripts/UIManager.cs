using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private BattleUI       battleUI;
    private CheckpointUI   checkpointUI;
    private CollectiblesUI collectiblesUI;
    private ControlsUI     controlsUI;
    private DialogueUI     dialogueUI;
    private InventoryUI    inventoryUI;
    private KeeperUI       keeperUI;
    private MainMenuUI     mainMenuUI;
    private NotificationUI notificationUI;
    private PauseUI        pauseUI;
    private PlayerHudUI    playerHudUI;
    private QuestLogUI     questLogUI;
    private ShopUI         shopUI;
    private WorldsUI       worldsUI;
    

    public enum UI
    {
        Battle,
        Checkpoint,
        Collectibles,
        Controls,
        Dialogue,
        Inventory,
        Keeper,
        MainMenu,
        Notification,
        PauseMenu,
        PlayerHud,
        QuestLog,
        Shop,
        Worlds,
    }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if (GameManager.Instance.getGameState() == GameManager.GameState.Play)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryUI.gameObject.activeInHierarchy)
                {
                    hideUI(UI.Inventory);
                }
                else
                {
                    showUI(UI.Inventory);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                if (questLogUI.gameObject.activeInHierarchy)
                {
                    hideUI(UI.QuestLog);
                }
                else
                {
                    showUI(UI.QuestLog);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameManager.Instance.getGameState() == GameManager.GameState.Pause)
                {
                    hideUI(UI.PauseMenu);
                    GameManager.Instance.setGameState(GameManager.GameState.Play);
                }
                else
                {
                    showUI(UI.PauseMenu);
                    GameManager.Instance.setGameState(GameManager.GameState.Pause);
                }
            }
        }
    }


    public void newNotification(string message)
    {
        notificationUI.showNotification(message);
    }


    public void newDialogue(string message)
    {
        dialogueUI.newDialogue(message);
    }


    public void addCombatLogMessage(string message)
    {
        battleUI.addCombatLogMessage(message);
    }


    public DialogueUI getDialogueUI()
    {
        return dialogueUI;
    }


    public void setUI(UI ui, MonoBehaviour uiScript)
    {
        switch (ui)
        {
            case UI.Battle:
                battleUI = (BattleUI)uiScript;
                break;

            case UI.Checkpoint:
                checkpointUI = (CheckpointUI)uiScript;
                break;

            case UI.Collectibles:
                collectiblesUI = (CollectiblesUI)uiScript;
                break;

            case UI.Controls:
                controlsUI = (ControlsUI)uiScript;
                break;

            case UI.Dialogue:
                dialogueUI = (DialogueUI)uiScript;
                break;

            case UI.Inventory:
                inventoryUI = (InventoryUI)uiScript;
                break;

            case UI.Keeper:
                keeperUI = (KeeperUI)uiScript;
                break;

            case UI.MainMenu:
                mainMenuUI = (MainMenuUI)uiScript;
                break;

            case UI.Notification:
                notificationUI = (NotificationUI)uiScript;
                break;

            case UI.PauseMenu:
                pauseUI = (PauseUI)uiScript;
                break;

            case UI.PlayerHud:
                playerHudUI = (PlayerHudUI)uiScript;
                break;

            case UI.QuestLog:
                questLogUI = (QuestLogUI)uiScript;
                break;

            case UI.Shop:
                shopUI = (ShopUI)uiScript;
                break;

            case UI.Worlds:
                worldsUI = (WorldsUI)uiScript;
                break;

            default:
                Debug.Log("ATTEMPT TO SET INVALID UI");
                break;
        }
    }


    public void updateUI(UI ui)
    {
        switch (ui)
        {
            case UI.Battle:
                battleUI.updateVisuals();
                break;

            case UI.PlayerHud:
                playerHudUI.updateVisuals();
                break;

            case UI.Shop:
                shopUI.updateVisuals();
                break;

            default:
                Debug.Log("ATTEMPT TO UPDATE INVALID UI");
                break;
        }
    }


    public void showUI(UI ui)
    {
        switch (ui)
        {
            case UI.Battle:
                battleUI.gameObject.SetActive(true);
                break;

            case UI.Collectibles:
                collectiblesUI.prepareCollectiblesShow();
                collectiblesUI.gameObject.SetActive(true);
                break;

            case UI.Checkpoint:
                checkpointUI.gameObject.SetActive(true);
                break;

            case UI.Controls:
                controlsUI.gameObject.SetActive(true);
                break;

            case UI.Dialogue:
                GameManager.Instance.setGameState(GameManager.GameState.Pause);
                dialogueUI.gameObject.SetActive(true);
                break;

            case UI.Inventory:
                if (inventoryUI.gameObject.active) break;
                inventoryUI.prepareInventoryShow();
                inventoryUI.gameObject.SetActive(true);
                break;

            case UI.Keeper:
                keeperUI.gameObject.SetActive(true);
                break;

            case UI.MainMenu:
                mainMenuUI.gameObject.SetActive(true);
                break;

            case UI.Notification:
                notificationUI.gameObject.SetActive(true);
                break;

            case UI.PauseMenu:
                pauseUI.gameObject.SetActive(true);
                break;

            case UI.PlayerHud:
                playerHudUI.gameObject.SetActive(true);
                break;

            case UI.QuestLog:
                questLogUI.prepareQuestLogShow();
                questLogUI.gameObject.SetActive(true);
                break;

            case UI.Shop:
                shopUI.prepareShopShow();
                shopUI.gameObject.SetActive(true);
                break;

            case UI.Worlds:
                worldsUI.gameObject.SetActive(true);
                break;

            default:
                Debug.Log("ATTEMPT TO SHOW INVALID UI");
                break;
        }
    }


    public void hideUI(UI ui)
    {
        switch (ui)
        {
            case UI.Battle:
                battleUI.gameObject.SetActive(false);
                break;

            case UI.Checkpoint:
                checkpointUI.gameObject.SetActive(false);
                break;

            case UI.Collectibles:
                collectiblesUI.prepareCollectiblesHide();
                collectiblesUI.gameObject.SetActive(false);
                break;

            case UI.Controls:
                controlsUI.gameObject.SetActive(false);
                break;

            case UI.Dialogue:
                dialogueUI.gameObject.SetActive(false);
                break;

            case UI.Inventory:
                inventoryUI.prepareInventoryHide();
                inventoryUI.gameObject.SetActive(false);
                break;

            case UI.Keeper:
                keeperUI.gameObject.SetActive(false);
                break;

            case UI.MainMenu:
                mainMenuUI.gameObject.SetActive(false);
                break;

            case UI.Notification:
                notificationUI.gameObject.SetActive(false);
                break;

            case UI.PauseMenu:
                pauseUI.gameObject.SetActive(false);
                break;

            case UI.PlayerHud:
                playerHudUI.gameObject.SetActive(false);
                break;

            case UI.QuestLog:
                questLogUI.prepareQuestLogHide();
                questLogUI.gameObject.SetActive(false);
                break;

            case UI.Shop:
                shopUI.prepareShopHide();
                shopUI.gameObject.SetActive(false);
                break;

            case UI.Worlds:
                worldsUI.gameObject.SetActive(false);
                break;

            default:
                Debug.Log("ATTEMPT TO HIDE INVALID UI");
                break;
        }
    }
}
