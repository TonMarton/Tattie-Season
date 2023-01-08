using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator;
    float gravityScale;

    private float horizontal;
    private bool isFacingRight = true;

    public UnityEvent OnJump;
    
    [Header("Config")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float accelaration = 0.2f;
    [SerializeField] private float deccelaration = 0.4f;
    [SerializeField] private float velPower = 2f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float fallGravityMultiplier = 2f;


    void Start()
    {
        gravityScale = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        float targetSpeed = horizontal * speed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accelaration : deccelaration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        rb.AddForce(movement * Vector2.right);
        //rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravityScale * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            OnJump?.Invoke();
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    
    //Pete: changed to public for the player footsteps to have access
    public bool IsGrounded()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        return isGrounded;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public bool IsMoving()
    {
        return horizontal != 0f;
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
