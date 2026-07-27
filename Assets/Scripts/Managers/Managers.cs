using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[RequireComponent(typeof(InventoryManager))]
public class Managers : MonoBehaviour
{
    public static InventoryManager Inventory { get; private set; }
 
    private readonly List<IGameManager> startSequence =
        new List<IGameManager>();
 
    private void Awake()
    {
        Inventory = GetComponent<InventoryManager>();
        startSequence.Add(Inventory);
 
        StartCoroutine(StartupManagers());
    }
 
    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in startSequence)
        {
            manager.Startup();
        }
 
        yield return null;
 
        int managerCount = startSequence.Count;
        int startedCount = 0;
 
        while (startedCount < managerCount)
        {
            int lastStartedCount = startedCount;
            startedCount = 0;
 
            foreach (IGameManager manager in startSequence)
            {
                if (manager.Status == ManagerStatus.Started)
                {
                    startedCount++;
                }
            }
 
            if (startedCount > lastStartedCount)
            {
                Debug.Log($"Progress: {startedCount}/{managerCount}");
            }
 
            yield return null;
        }
 
        Debug.Log("All managers started.");
    }
}