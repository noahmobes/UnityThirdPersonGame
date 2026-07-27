using UnityEngine;
 
public class CollectibleItem : MonoBehaviour
{
    [SerializeField]
    private string itemName;
 
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