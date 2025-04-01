using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    // Where we declare variables
    public float moveSpeed = 2;
    public float jumpForce = 4;

    public LayerMask whatIsGround;

    // Controlled by left/right arrows
    private float moveDirection;
    
    // A simple check for animation
    public bool isGrounded;
    
    // Components of my player
    private Rigidbody2D playerRb;
    private SpriteRenderer spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerRb.linearVelocityX = moveDirection * moveSpeed;
        
        CheckIfGrounded();
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 value = inputValue.Get<Vector2>();
        moveDirection = value.x;

        // Flip the sprite ONLY when the 
        // left/right keys are pressed
        if (moveDirection != 0)
        {
            spriteRenderer.flipX = moveDirection < 0;
        }
    }

    private void OnJump(InputValue inputValue)
    {
        if (isGrounded)
        {
            playerRb.linearVelocityY = jumpForce;
        }
    }

    private void CheckIfGrounded()
    {
        // Project a box down towards the ground
        // If it hits anything with a collider
        // then the character is grounded
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position, // origin point
            Vector2.one * 0.1f, // box size
            0, // angle of rotation
            Vector2.down, // cast direction
            0.2f, // maximum distance
            whatIsGround.value
        );

        // If we found an object, we are grounded
        isGrounded = hit.collider != null;
    }
}
