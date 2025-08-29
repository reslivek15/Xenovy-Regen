using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Rotation : MonoBehaviour
{

    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerModel;
    [SerializeField] Rigidbody rb;
    [SerializeField] float rotationSpeed;
    [SerializeField] Camera cam; 

    private Vector2 moveInput;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        ///Debug.Log($"Nigga Move Input: {moveInput}");
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        
        Vector3 cameraForward = cam.transform.forward;
        cameraForward.y = 0;
        orientation.forward = cameraForward.normalized;

        float horInput = moveInput.x;
        float vertInput = moveInput.y;
        
        Vector3 inputDir = orientation.forward * vertInput + orientation.right * horInput;
        
        if (inputDir != Vector3.zero)
        {
            Vector3 targetDirection = inputDir.normalized;

            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

                playerModel.rotation = Quaternion.Slerp(playerModel.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
