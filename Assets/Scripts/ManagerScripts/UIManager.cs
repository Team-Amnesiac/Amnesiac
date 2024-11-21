using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private BattleUI battleUI;
    [SerializeField] private CollectiblesUI collectiblesUI;
    //[SerializeField] private ControlsUI controlsUI;
    //[SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private KeeperUI keeperUI;
    //[SerializeField] private MainMenuUI mainMenuUI;
    //[SerializeField] private PauseMenuUI pauseMenuUI;
    [SerializeField] private PlayerHudUI playerHudUI;
    [SerializeField] private QuestLogUI questLogUI;
    [SerializeField] private ShopUI shopUI;
    

    public enum UI
    {
        Battle,
        Collectibles,
        Controls,
        Dialogue,
        Inventory,
        Keeper,
        MainMenu,
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

            //case UI.Controls:
            //    controlsUI.show();
            //    break;

            //case UI.Dialogue:
            //    dialogueUI.show();
            //    break;

            case UI.Inventory:
                inventoryUI = (InventoryUI)uiScript;
                break;

            case UI.Keeper:
                keeperUI = (KeeperUI)uiScript;
                break;

            //case UI.MainMenu:
            //    mainMenuUI.show();
            //    break;

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


    public void showUI(UI ui)
    {
        switch (ui)
        {
            case UI.Battle:
                battleUI.show();
                break;

            case UI.Collectibles:
                collectiblesUI.show();
                break;

            //case UI.Controls:
            //    controlsUI.show();
            //    break;

            //case UI.Dialogue:
            //    dialogueUI.show();
            //    break;

            case UI.Inventory:
                inventoryUI.show();
                break;

            case UI.Keeper:
                keeperUI.show();
                break;

            //case UI.MainMenu:
            //    mainMenuUI.show();
            //    break;

            //case UI.PauseMenu:
            //    pauseMenuUI.show();
            //    break;

            case UI.PlayerHud:
                playerHudUI.show();
                break;

            case UI.QuestLog:
                questLogUI.show();
                break;

            case UI.Shop:
                shopUI.show();
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
                battleUI.hide();
                break;

            case UI.Collectibles:
                collectiblesUI.hide();
                break;

            //case UI.Controls:
            //    controlsUI.hide();
            //    break;

            //case UI.Dialogue:
            //    dialogueUI.hide();
            //    break;

            case UI.Inventory:
                inventoryUI.hide();
                break;

            case UI.Keeper:
                keeperUI.hide();
                break;

            //case UI.MainMenu:
            //    mainMenuUI.hide();
            //    break;

            //case UI.PauseMenu:
            //    pauseMenuUI.hide();
            //    break;

            case UI.PlayerHud:
                playerHudUI.hide();
                break;

            case UI.QuestLog:
                questLogUI.hide();
                break;

            case UI.Shop:
                shopUI.hide();
                break;

            default:
                Debug.Log("ATTEMPT TO HIDE INVALID UI");
                break;
        }
    }
}
