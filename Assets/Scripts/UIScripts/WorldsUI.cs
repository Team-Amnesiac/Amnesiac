using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WorldsUI : MonoBehaviour
{
    [SerializeField] private Button hubButton;
    [SerializeField] private Button noryxButton;
    [SerializeField] private Button loikartButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Worlds, this);
        UIManager.Instance.hideUI(UIManager.UI.Worlds);

        disableAppropriateWorldButtons();

        hubButton.onClick.AddListener(hubButtonOnClick);
        noryxButton.onClick.AddListener(noryxButtonOnClick);
        loikartButton.onClick.AddListener(loikartButtonOnClick);
        exitButton.onClick.AddListener(exitButtonOnClick);
    }

    private void DisableButton(Button button)
    {
        button.interactable = false;
        ColorBlock colors = button.colors;
        colors.normalColor = Color.grey;
        button.colors = colors;
    }

    private void hubButtonOnClick()
    {
        SceneLoader.Instance.loadScene(SceneLoader.Scene.Hub);
    }

    private void noryxButtonOnClick()
    {
        SceneLoader.Instance.loadScene(SceneLoader.Scene.Noryx);
    }

    private void loikartButtonOnClick()
    {
        SceneLoader.Instance.loadScene(SceneLoader.Scene.Loikart);
    }

    private void exitButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.Worlds);
        UIManager.Instance.showUI(UIManager.UI.Checkpoint);
    }

    private void disableAppropriateWorldButtons()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Hub")
        {
            DisableButton(hubButton);
        }
            
        if (currentScene == "Noryx" ||
            (!QuestManager.Instance.getActiveQuests().Contains(QuestManager.Instance.SecondQuest) &&
             !QuestManager.Instance.getCompletedQuests().Contains(QuestManager.Instance.SecondQuest)))
        {
            DisableButton(noryxButton);
        }

        if (currentScene == "Loikart" ||
            (!QuestManager.Instance.getActiveQuests().Contains(QuestManager.Instance.ThirdQuest) &&
             !QuestManager.Instance.getCompletedQuests().Contains(QuestManager.Instance.ThirdQuest)))
        {
            DisableButton(loikartButton);
        }
    }
}
