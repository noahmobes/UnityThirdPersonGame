using System.Collections.Generic;
using TMPro;
using UnityEngine;
 
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform itemContainer;
    // [SerializeField] private TextMeshProUGUI itemTextPrefab;
    // [SerializeField] private TextMeshProUGUI equippedItemText;
    [SerializeField] private TextMeshProUGUI keyNumText;
 
    private void Update()
    {
        UpdateInventoryDisplay();
    }
 
    private void UpdateInventoryDisplay()
    {
        if (Managers.Inventory == null)
        {
            return;
        }
 
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        keyNumText.text = Managers.Inventory.GetItemCount("key").ToString();
 
        // List<string> itemList = Managers.Inventory.GetItemList();
 
        // if (itemList.Count == 0)
        // {
        //     TextMeshProUGUI emptyText =
        //         Instantiate(itemTextPrefab, itemContainer);
 
        //     emptyText.text = "No Items";
        // }
        // else
        // {
        //     foreach (string item in itemList)
        //     {
        //         int count = Managers.Inventory.GetItemCount(item);
 
        //         TextMeshProUGUI itemText =
        //             Instantiate(itemTextPrefab, itemContainer);
 
        //         itemText.text = $"{item} ({count})";
        //     }
        // }
 
        // string equipped = Managers.Inventory.EquippedItem;
 
        // if (string.IsNullOrEmpty(equipped))
        // {
        //     equippedItemText.text = "";
        // }
        // else
        // {
        //     equippedItemText.text = $"Equipped: {equipped}";
        // }
    }
}