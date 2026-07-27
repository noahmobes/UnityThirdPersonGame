using System.Collections;
using UnityEngine;
public class LockedDoor :
    MonoBehaviour,
    IInteractable
{
    [SerializeField]
    private float openAngle = 90f;
    [SerializeField]
    private float openSpeed = 2f;
    [SerializeField]
    private float moveSpeed = 2f;
    private Vector3 openOffset = new Vector3(0f, 3f, 0f);
    private bool isOpen;
    private bool isMoving;
    public void Interact()
    {      
        // Will not trigger if the door is already moving or if the player does not have a key in their inventory
        if (isMoving || !Managers.Inventory.HasItem("key"))
        {
            return;
        }
        StartCoroutine(ToggleDoor());
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