using UnityEngine;
using UnityEngine.SceneManagement;
 
public class ExitZone : MonoBehaviour
{
    [SerializeField]
    private GameObject victoryItem;
    public string endSceneName = "EndScene";
    private void OnTriggerEnter(Collider other)
    {
        // if (other.GetComponent<PlayerInteraction>() && Managers.Inventory.HasItem(victoryItem.name))
        // {
        //     SceneManager.LoadScene(endSceneName);
        // }

        if (other.tag == "Player" && Managers.Inventory.HasItem(victoryItem.name))
        {
            SceneManager.LoadScene(endSceneName);
        }
    }
}