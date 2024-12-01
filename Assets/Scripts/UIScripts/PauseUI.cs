using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button exitButton;


    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.PauseMenu, this);
        UIManager.Instance.hideUI(UIManager.UI.PauseMenu);
        continueButton.onClick.AddListener(continueButtonOnClick);
        loadButton.onClick.AddListener(loadButtonOnClick);
        exitButton.onClick.AddListener(exitButtonOnClick);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    public void LoadGame()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Loading);
    }


    private void continueButtonOnClick()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Play);
        UIManager.Instance.hideUI(UIManager.UI.PauseMenu);
    }


    private void loadButtonOnClick()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Loading);
        UIManager.Instance.hideUI(UIManager.UI.PauseMenu);
    }


    private void exitButtonOnClick()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Title);
        UIManager.Instance.hideUI(UIManager.UI.PauseMenu);
    }
}
