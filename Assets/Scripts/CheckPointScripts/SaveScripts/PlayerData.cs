// Makes the class serializable so it can be converted to and from JSON or other data formats.
[System.Serializable]

// For Saving Player Related Data
public class PlayerData
{
    // Stores the player's current health value.
    public float health;

    // Stores the player's current level.
    public int level;

    // Stores the player's stamina, which could be used for actions like running or using skills.
    public int stamina;

    // Stores the player's total in-game currency.
    public int currency;

    // Stores the player's current experience points.
    public float experience;

    // Stores the ID of the last checkpoint the player reached, used for respawning.
    public string checkpointID;
}
