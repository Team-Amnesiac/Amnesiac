using System.Collections.Generic;

//Mark the class as serializable so it can be converted to and from JSON or other data formats.
[System.Serializable]

// For Saving Inventory Items Data
public class InventoryData
{
    // A list of strings to store the names or IDs of items in the player's inventory.
    public List<string> items;
}