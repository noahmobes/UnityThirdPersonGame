using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
 
public class CollectibleItem : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private string itemName;

    private void Start()
    {
        itemName = prefab.name;
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }
 
        Managers.Inventory.AddItem(itemName);
 
        Destroy(gameObject);
    }
}