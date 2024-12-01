using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WorldsUI : MonoBehaviour
{
    public Button hubButton;
    public Button noryxButton;
    public Button loikartButton;
    public Button exitButton;
    public GameObject checkpointUI;

    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Hub")
            DisableButton(hubButton);
        else if (currentScene == "Noryx")
            DisableButton(noryxButton);
        else if (currentScene == "Loikart")
            DisableButton(loikartButton);
    }

    private void DisableButton(Button button)
    {
        button.interactable = false;
        ColorBlock colors = button.colors;
        colors.normalColor = Color.grey;
        button.colors = colors;
    }

    public void LoadHub()
    {
        ChangeScene("Hub");
    }

    public void LoadNoryx()
    {
        ChangeScene("Noryx");
    }

    public void LoadLoikart()
    {
        ChangeScene("Loikart");
    }

    public void BackToCheckpointUI()
    {
        gameObject.SetActive(false);
        checkpointUI.SetActive(true);
    }


    private void ChangeScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log($"{sceneName} is already the current scene!");
        }
    }
}
