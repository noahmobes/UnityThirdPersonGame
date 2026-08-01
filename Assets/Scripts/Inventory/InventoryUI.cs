using TMPro;
using UnityEngine;
 
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform itemContainer;
    [SerializeField] private TextMeshProUGUI keyNumText;
    [SerializeField] private TextMeshProUGUI cubeNumText;
 
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
 
        // foreach (Transform child in itemContainer)
        // {
        //     Destroy(child.gameObject);
        // }

        keyNumText.text = Managers.Inventory.GetItemCount("key").ToString();
        cubeNumText.text = Managers.Inventory.GetItemCount("cube").ToString();
    }
}