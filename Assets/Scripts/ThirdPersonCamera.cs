using UnityEngine;
using UnityEngine.InputSystem;
 
public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;
 
    [Header("Camera Settings")]
    [SerializeField] private float rotationSpeed = 90f;
 
    public Vector3 cameraLookOffset = new Vector3(0f, 1.5f, 0f);
    private float cameraYaw;
    private Vector3 cameraOffset;
 
    private InputAction lookAction;
 
    private void Awake()
    {
        lookAction = InputSystem.actions.FindAction("Look");
 
        if (lookAction == null)
        {
            Debug.LogError(
                "Could not find a 'Look' action in InputSystem_Actions.");
        }
    }
 
    private void Start()
    {
        cameraYaw = transform.eulerAngles.y;
        cameraOffset = player.position - transform.position;
    }
 
    private void LateUpdate()
    {
        if (player == null || lookAction == null)
        {
            return;
        }
 
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
 
        cameraYaw += lookInput.x * rotationSpeed * Time.deltaTime;
 
        Quaternion cameraRotation =
            Quaternion.Euler(0f, cameraYaw, 0f);
 
        transform.position =
            player.position - (cameraRotation * cameraOffset);
 
        transform.LookAt(player.position + cameraLookOffset);
    }
}