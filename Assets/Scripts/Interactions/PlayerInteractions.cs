using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInteraction : MonoBehaviour
{
    private readonly List<IInteractable> nearbyInteractables =
        new List<IInteractable>();
    private InputAction interactAction;
    private void Awake()
    {
        interactAction =
            InputSystem.actions.FindAction("Interact");
        if (interactAction == null)
        {
            Debug.LogError(
                "Could not find an 'Interact' action in InputSystem_Actions.");
        }
    }
    private void Update()
    {
        if (interactAction == null)
        {
            return;
        }
        if (interactAction.WasPressedThisFrame())
        {
            ActivateClosestInteractable();
        }
    }
    private void ActivateClosestInteractable()
    {
        if (nearbyInteractables.Count == 0)
        {
            return;
        }
        if (nearbyInteractables.Count == 1)
        {
            nearbyInteractables[0].Interact();
            return;
        }
        IInteractable closestInteractable = null;
        float closestDistance = Mathf.Infinity;
        foreach (IInteractable interactable in nearbyInteractables)
        {
            MonoBehaviour behaviour =
                interactable as MonoBehaviour;
            if (behaviour == null)
            {
                continue;
            }
            float distance =
                Vector3.Distance(
                    transform.position,
                    behaviour.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractable = interactable;
            }
        }
        closestInteractable?.Interact();
    }
    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable =
            other.GetComponent<IInteractable>();
        if (interactable != null &&
            !nearbyInteractables.Contains(interactable))
        {
            nearbyInteractables.Add(interactable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable =
            other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            nearbyInteractables.Remove(interactable);
        }
    }
}