using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    [SerializeField] float groundDrag;

    public float playeHeight;
    public LayerMask ground;
    bool grounded;

    [SerializeField] Transform orientation;

    private Rigidbody rb;
    private Vector2 moveInput;
    private Vector3 moveDir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();   
        rb.freezeRotation = true;
    }

    public void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playeHeight * 0.5f + 0.2f, ground);

        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

        VelocityLimiter();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Move Input: {moveInput}");
    }


     void FixedUpdate()
    {
        ///Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        moveDir = orientation.forward * moveInput.y + orientation.right * moveInput.x;
        if (grounded)
        {
            rb.AddForce(moveDir.normalized * speed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDir.normalized * speed * 10f * 0.4f, ForceMode.Force);
        }

       /// moveDir.y += gravity * Time.deltaTime;
    }

    private void VelocityLimiter()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVelocity.magnitude > speed)
        {
            Vector3 limitedVel = flatVelocity.normalized * speed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

}
