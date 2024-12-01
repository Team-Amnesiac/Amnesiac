using UnityEngine;
using UnityEngine.UI;

public class CheckpointUI : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button worldsButton;
    [SerializeField] private Button exitButton;


    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Checkpoint, this);
        UIManager.Instance.hideUI(UIManager.UI.Checkpoint);
        saveButton.onClick.AddListener(saveButtonOnClick);
        loadButton.onClick.AddListener(loadButtonOnClick);
        worldsButton.onClick.AddListener(worldsButtonOnClick);
        exitButton.onClick.AddListener(exitButtonOnClick);
    }

    public void saveButtonOnClick()
    {
        SaveSystem.SaveGame();
        Debug.Log("Game saved successfully!");
    }

    public void loadButtonOnClick()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Loading);
        UIManager.Instance.hideUI(UIManager.UI.Checkpoint);
        Debug.Log("Game loaded successfully!");
    }

    public void worldsButtonOnClick()
    {
        UIManager.Instance.showUI(UIManager.UI.Worlds);
        UIManager.Instance.hideUI(UIManager.UI.Checkpoint);
    }


    private void exitButtonOnClick()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Play);
        UIManager.Instance.hideUI(UIManager.UI.Checkpoint);
    }
}
