using UnityEngine;

public class CheckpointUI : MonoBehaviour
{
    public GameObject worldsUI;
    public GameObject checkpointUI;

    public void SaveGame()
    {
        SaveSystem.SaveGame();
        Debug.Log("Game saved successfully!");
    }

    public void LoadGame()
    {
        SaveSystem.LoadGame();
        Debug.Log("Game loaded successfully!");
    }

    public void OpenWorldsUI()
    {
        worldsUI.SetActive(true);
        checkpointUI.SetActive(false);
        Time.timeScale = 0f;
    }

    public void CloseUI()
    {
        worldsUI.SetActive(false);
        checkpointUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
