using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private BattleUI       battleUI;
    private CollectiblesUI collectiblesUI;
    private ControlsUI     controlsUI;
    //private DialogueUI dialogueUI;
    private InventoryUI    inventoryUI;
    private KeeperUI       keeperUI;
    private MainMenuUI     mainMenuUI;
    private NotificationUI notificationUI;
    //private PauseMenuUI pauseMenuUI;
    private PlayerHudUI    playerHudUI;
    private QuestLogUI     questLogUI;
    private ShopUI         shopUI;
    

    public enum UI
    {
        Battle,
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
        }
    }


    public void newNotification(string message)
    {
        notificationUI.showNotification(message);
    }


    public void addCombatLogMessage(string message)
    {
        battleUI.addCombatLogMessage(message);
    }


    public void setUI(UI ui, MonoBehaviour uiScript)
    {
        switch (ui)
        {
            case UI.Battle:
                battleUI = (BattleUI)uiScript;
                break;

            case UI.Collectibles:
                collectiblesUI = (CollectiblesUI)uiScript;
                break;

            case UI.Controls:
                controlsUI = (ControlsUI)uiScript;
                break;

            //case UI.Dialogue:
            //    dialogueUI.show();
            //    break;

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

            //case UI.PauseMenu:
            //    pauseMenuUI.show();
            //    break;

            case UI.PlayerHud:
                playerHudUI = (PlayerHudUI)uiScript;
                break;

            case UI.QuestLog:
                questLogUI = (QuestLogUI)uiScript;
                break;

            case UI.Shop:
                shopUI = (ShopUI)uiScript;
                break;

            default:
                Debug.Log("ATTEMPT TO SHOW INVALID UI");
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

            default:
                Debug.Log("ATTEMPT TO SHOW INVALID UI");
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
                collectiblesUI.gameObject.SetActive(true);
                break;

            case UI.Controls:
                controlsUI.gameObject.SetActive(true);
                break;

            //case UI.Dialogue:
            //    dialogueUI.show();
            //    break;

            case UI.Inventory:
                inventoryUI.prepareInventoryShow();
                inventoryUI.gameObject.SetActive(true);
                break;

            case UI.Keeper:
                keeperUI.gameObject.SetActive(true);
                break;

            case UI.MainMenu:
                mainMenuUI.gameObject.SetActive(true);
                break;

            //case UI.PauseMenu:
            //    pauseMenuUI.show();
            //    break;

            case UI.PlayerHud:
                playerHudUI.gameObject.SetActive(true);
                break;

            case UI.QuestLog:
                questLogUI.gameObject.SetActive(true);
                break;

            case UI.Shop:
                shopUI.gameObject.SetActive(true);
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

            case UI.Collectibles:
                collectiblesUI.gameObject.SetActive(false);
                break;

            case UI.Controls:
                controlsUI.gameObject.SetActive(false);
                break;

            //case UI.Dialogue:
            //    dialogueUI.hide();
            //    break;

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

            //case UI.PauseMenu:
            //    pauseMenuUI.hide();
            //    break;

            case UI.PlayerHud:
                playerHudUI.gameObject.SetActive(false);
                break;

            case UI.QuestLog:
                questLogUI.gameObject.SetActive(false);
                break;

            case UI.Shop:
                shopUI.gameObject.SetActive(false);
                break;

            default:
                Debug.Log("ATTEMPT TO HIDE INVALID UI");
                break;
        }
    }
}
