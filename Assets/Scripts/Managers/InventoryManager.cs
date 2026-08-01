using System.Collections.Generic;
using UnityEngine;
public class InventoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }
    private readonly Dictionary<string, int> items =
        new Dictionary<string, int>();
    public void Startup()
    {
        Debug.Log("Inventory manager starting.");
        items.Clear();
        Status = ManagerStatus.Started;
    }
    public void AddItem(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName]++;
        }
        else
        {
            items[itemName] = 1;
        }
        Debug.Log($"Added item: {itemName}");
        DisplayItems();
    }
    public bool ConsumeItem(string itemName)
    {
        if (!items.ContainsKey(itemName))
        {
            Debug.Log($"Cannot consume {itemName}. Item not found.");
            return false;
        }
        items[itemName]--;
        if (items[itemName] <= 0)
        {
            items.Remove(itemName);
        }
        DisplayItems();
        return true;
    }
    public bool HasItem(string itemName)
    {
        return items.ContainsKey(itemName);
    }
    public int GetItemCount(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            return items[itemName];
        }
        return 0;
    }
    public List<string> GetItemList()
    {
        return new List<string>(items.Keys);
    }
    
    private void DisplayItems()
    {
        string itemDisplay = "Inventory: ";
        foreach (KeyValuePair<string, int> item in items)
        {
            itemDisplay += $"{item.Key}({item.Value}) ";
        }
        Debug.Log(itemDisplay);
    }
}