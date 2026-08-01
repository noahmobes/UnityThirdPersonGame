using UnityEngine;
using UnityEngine.InputSystem;
 
[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform cameraTransform;
 
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;
 
    private CharacterController characterController;
    private InputAction moveAction;
    private InputAction jumpAction;
    private float verticalVelocity;
    private Animator animator;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
 
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
 
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
 
        if (moveAction == null)
        {
            Debug.LogError(
                "Could not find a 'Move' action in InputSystem_Actions.");
        }
        if (jumpAction == null)
        {
            Debug.LogError("Could not find a 'Jump' action in InputSystem_Actions.");
        }
    }
 
    private void Update()
    {        
        if (cameraTransform == null || moveAction == null || jumpAction == null)
        {
            return;
        }
 
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        animator.SetFloat("Speed", moveInput.magnitude);
        Vector3 movementDirection = Vector3.zero;
 
        if (moveInput.sqrMagnitude > 0.01f)
        {
            // Get the camera's horizontal directions.
            Vector3 cameraRight = cameraTransform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();
 
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            movementDirection = (cameraRight * moveInput.x) + (cameraForward * moveInput.y);
 
            movementDirection.Normalize();
            
            verticalVelocity += gravity * Time.deltaTime;
            
            Vector3 finalMovement =
                (movementDirection * movementSpeed) +
                (Vector3.up * verticalVelocity);
            
            characterController.Move(finalMovement * Time.deltaTime);
 
            // Turn the character to face the direction of movement.
            transform.rotation =
                Quaternion.LookRotation(movementDirection);
        }

        bool isGrounded = characterController.isGrounded;
        animator.SetBool("IsGrounded", isGrounded);
        
        if (characterController.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }
        
        if (characterController.isGrounded && jumpAction.WasPressedThisFrame())
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}