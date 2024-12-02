// Makes the class serializable for saving and loading data.
[System.Serializable]

// For Storing Save Data
public class SaveData
{
    // Stores the player's save data (e.g., health, level, experience, etc.).
    public PlayerData playerData;

    // Stores the player's active and completed quests.
    public QuestData questData;

    // Stores the player's inventory items.
    public InventoryData inventoryData;

    // Stores the player's collectible progress (e.g., trophies, active/completed sets).
    public CollectiblesData collectiblesData;

    // Stores the name of the current scene to reload it during game loading.
    public string sceneName;
}