using System.IO;
using UnityEngine;

// Static class that is responsible for handling game save and load functionality.
public static class SaveSystem
{
    // Path where the save file is stored. The `persistentDataPath` is a safe location for saving game data.
    private static string saveFilePath = Application.persistentDataPath + "/saveData.json"; 

    // Method for saving the current game state.
    public static void SaveGame()
    {
        // Create an instance of SaveData to store all relevant game data.
        SaveData saveData = new SaveData
        {
            // Save the current state of the player (e.g., position, health).
            playerData = PlayerManager.Instance.SaveState(),
             // Save the state of collected items.
            collectiblesData = CollectibleManager.Instance.SaveState(),
             // Save the state of all active and completed quests.
            questData = QuestManager.Instance.SaveState(),
             // Save the name of the currently active scene.
            sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
            // Save the state of the player's inventory.
            inventoryData = InventoryManager.Instance.SaveState()
        };

        // Convert the SaveData object to a JSON-formatted string for easy storage.
        string json = JsonUtility.ToJson(saveData, true); 

        // Write the JSON string to the save file at the specified path.
        File.WriteAllText(saveFilePath, json); 

        // Log a message to confirm the game was saved successfully.
        Debug.Log($"Game saved to {saveFilePath}"); 
    }

    // Method for Loading the saved game state.
    public static void LoadGame()
    {
        if (File.Exists(saveFilePath)) 
        // Check if the save file exists before attempting to load it.
        {
            string json = File.ReadAllText(saveFilePath); 
            // Read the contents of the save file.

            SaveData saveData = JsonUtility.FromJson<SaveData>(json); 
            // Deserialize the JSON string into a SaveData object.

            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != saveData.sceneName) 
            // Check if the current scene matches the saved scene name.
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(saveData.sceneName); 
                // Load the saved scene if it doesn't match the current one.
            }

            PlayerManager.Instance.LoadState(saveData.playerData); 
            // Restore the player's state from the saved data.
            CollectibleManager.Instance.LoadState(saveData.collectiblesData); 
            // Restore the collectibles' state from the saved data.
            QuestManager.Instance.LoadState(saveData.questData); 
            // Restore the quests' state from the saved data.
            GameManager.Instance.setGameState(GameManager.GameState.Play); 
            // Set the game state back to 'Play' mode after loading.
            InventoryManager.Instance.LoadState(saveData.inventoryData); 
            // Restore the inventory state from the saved data.

            Debug.Log("Game loaded successfully."); 
            // Log a message to confirm the game was loaded successfully.
        }
        else
        {
            Debug.LogWarning("Save file not found."); 
            // Log a warning if the save file does not exist.
        }
    }
}
