using System.Collections;
using UnityEngine;
public class LockedDoor :
    MonoBehaviour,
    IInteractable
{
    [SerializeField]
    private float openSpeed = 2f;
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private GameObject requiredItemPrefab;
    [SerializeField]
    private int requiredItemCount = 1;
    [SerializeField]
    private bool consumeRequiredItem = true;
    private Vector3 openOffset = new Vector3(0f, 3f, 0f);
    private bool isOpen;
    private bool isMoving;
    private bool isLocked = true;
    public void Interact()
    {      
        // Will not trigger if the door is already moving or if the player does not have the required item in their inventory
        if (isMoving || (!Managers.Inventory.HasItem(requiredItemPrefab.name) && isLocked))
        {
            return;
        }

        if (isLocked)
        {
            if (Managers.Inventory.GetItemCount(requiredItemPrefab.name) >= requiredItemCount)
            {
                if (consumeRequiredItem)
                {
                    for (int i = 0; i < requiredItemCount; i++)
                    {
                        Managers.Inventory.ConsumeItem(requiredItemPrefab.name);
                    }
                }
                StartCoroutine(ToggleDoor());
                isLocked = false;
            } else
            {
                return;
            }
        }
        else
        {
            StartCoroutine(ToggleDoor());
        }
        
        
    }

    private IEnumerator ToggleDoor()
    {
        isMoving = true;
        float progress = 0f;
        if (isOpen)
        {
            openOffset = -openOffset;
        } else
        {
            openOffset = new Vector3(0f, 3f, 0f);
        }
        while (progress < 1f)
        {
            Vector3 finalPosition = transform.position + openOffset;
            while (Vector3.Distance(transform.position, finalPosition) > 0.01f)
            {
                transform.position =
                    Vector3.MoveTowards(
                        transform.position,
                        finalPosition,
                        moveSpeed * Time.deltaTime);
                    progress += Time.deltaTime * openSpeed;
                yield return null;
            }
            transform.position = finalPosition;
        }
        isOpen = !isOpen;
        isMoving = false;
    }
}