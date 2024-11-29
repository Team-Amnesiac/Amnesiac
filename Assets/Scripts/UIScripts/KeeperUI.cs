using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeeperUI : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button talkButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button collectiblesButton;
    [SerializeField] private KeeperInteraction keeperInteraction;
    
    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Keeper, this);
        UIManager.Instance.hideUI(UIManager.UI.Keeper);

        exitButton.onClick.AddListener(exitButtonOnClick);
        talkButton.onClick.AddListener(talkButtonOnClick);
        shopButton.onClick.AddListener(shopButtonOnClick);
        collectiblesButton.onClick.AddListener(collectiblesButtonOnClick);
    }


    private void exitButtonOnClick()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Play);
        UIManager.Instance.hideUI(UIManager.UI.Keeper);
    }


    private void talkButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.Keeper);
        UIManager.Instance.showUI(UIManager.UI.Dialogue);
        keeperInteraction.talkTo();
    }


    private void shopButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.Keeper);
        UIManager.Instance.showUI(UIManager.UI.Shop);
    }


    private void collectiblesButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.Keeper);
        UIManager.Instance.showUI(UIManager.UI.Collectibles);
    }
}
