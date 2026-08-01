using UnityEngine;
using UnityEngine.SceneManagement;
 
public class VictoryZone : MonoBehaviour
{
    [SerializeField]
    private GameObject victoryItem;
    public string endSceneName = "EndScene";
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Managers.Inventory.HasItem(victoryItem.name))
        {
            SceneManager.LoadScene(endSceneName);
        }
    }
}