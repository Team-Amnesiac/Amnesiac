using UnityEngine;

public class PauseUI : MonoBehaviour
{
    private bool isPaused = false;

    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.PauseMenu, this);
        UIManager.Instance.hideUI(UIManager.UI.PauseMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        UIManager.Instance.hideUI(UIManager.UI.PauseMenu);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        UIManager.Instance.showUI(UIManager.UI.PauseMenu);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    public void LoadGame()
    {
        SaveSystem.LoadGame();
        ResumeGame();
    }
}
