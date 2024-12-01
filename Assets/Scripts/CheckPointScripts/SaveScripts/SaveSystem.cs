using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string saveFilePath = Application.persistentDataPath + "/saveData.json";
    
    public static void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerData = PlayerManager.Instance.SaveState(),
            collectiblesData = CollectibleManager.Instance.SaveState(),
            questData = QuestManager.Instance.SaveState(),
            sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        };

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(saveFilePath, json);

        Debug.Log($"Game saved to {saveFilePath}");
    }

    // Loads the game state (include this for pause menu but currently its within checkpointUI)
    public static void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != saveData.sceneName)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(saveData.sceneName);
            }

            PlayerManager.Instance.LoadState(saveData.playerData);
            CollectibleManager.Instance.LoadState(saveData.collectiblesData);
            QuestManager.Instance.LoadState(saveData.questData);
            GameManager.Instance.setGameState(GameManager.GameState.Play);

            Debug.Log("Game loaded successfully.");
        }
        else
        {
            Debug.LogWarning("Save file not found.");
        }
    }

}
