using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private Animator animator;
    private bool isWalking = false;

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

        animator.SetFloat("speed", speed);

        VelocityLimiter();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        //Debug.Log($"Move Input: {moveInput}");

        if( moveInput == Vector2.zero)
        {
            speed = 0;
            isWalking = false;
            animator.SetBool("isWalking", false);
        }
        else
        {
            if (speed < walkSpeed)
            {
                speed = walkSpeed;
                isWalking = true;
                animator.SetBool("isWalking", true);
            }
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed && isWalking == true)
        {
            speed = runSpeed;
        }
        else if (context.canceled)
        {
            if (isWalking == true)
            {
                speed = walkSpeed;
            }
            else
            {
                speed = 0;
            }            
        }

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
