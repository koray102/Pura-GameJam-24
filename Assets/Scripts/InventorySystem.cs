using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // Dictionary to store items and their counts
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    // Function to add an item to the inventory
    public void AddItem(string itemName, int count = 1)
    {
        if (inventory.ContainsKey(itemName))
        {
            // If item already exists, increase its count
            inventory[itemName] += count;
        }
        else
        {
            // If item doesn't exist, add it to the inventory
            inventory.Add(itemName, count);
        }
    }

    // Function to remove an item from the inventory
    public void RemoveItem(string itemName, int count = 1)
    {
        if (inventory.ContainsKey(itemName))
        {
            // Decrease the count of the item
            inventory[itemName] -= count;

            // If count becomes zero or less, remove the item from inventory
            if (inventory[itemName] <= 0)
            {
                inventory.Remove(itemName);
            }
        }
    }

    // Function to check if an item exists in the inventory
    public bool HasItem(string itemName)
    {
        return inventory.ContainsKey(itemName);
    }

    // Function to get the count of an item in the inventory
    public int GetItemCount(string itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            return inventory[itemName];
        }
        else
        {
            return 0;
        }
    }

    // Function to display the inventory contents
    public void DisplayInventory()
    {
        Debug.Log("Inventory Contents:");
        foreach (var item in inventory)
        {
            Debug.Log(item.Key + ": " + item.Value);
        }
    }

    // Example usage
    private void Start()
    {
        // Add some items to the inventory
        AddItem("Sword", 3);
        AddItem("Potion", 5);

        // Display the inventory
        DisplayInventory();

        // Remove some items from the inventory
        RemoveItem("Sword", 2);
        RemoveItem("Potion");

        // Display the updated inventory
        DisplayInventory();
    }
}
