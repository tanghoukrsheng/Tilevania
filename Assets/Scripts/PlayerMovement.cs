using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 8f; 
    [SerializeField] float jumpSpeed = 12f; 
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator myAnimator;

    CapsuleCollider2D myCapsuleCollider;
    // alled once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        Run();
        FlipSprite();
    }


// this method is called by the Input System when the "Move" action is triggered, and it receives an InputValue object that contains the current value of the input action.
    //  In this case, we expect the "Move" action to be a Vector2 (e.g., from a joystick or WASD keys), 
    // so we call value.Get<Vector2>() to retrieve that value and store it in the moveInput variable for later use in the Update method or elsewhere in the script.
    void OnMove(InputValue value) 
    {
        moveInput = value.Get<Vector2>();
        
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb.linearVelocity.y); 
        rb.linearVelocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed );
    }

    void FlipSprite()
    {
        // Mathf.Epsilon is a very small number used to compare floating-point values. 
        // It helps to avoid issues with precision when checking if the player's horizontal speed is effectively zero.
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon; 
        if (playerHasHorizontalSpeed)
        {
            // Mathf.Sign returns -1, 0, or 1 depending on the sign of the input value.
            //  In this case, it will return -1 if the player is moving left (negative velocity) and 1 if the player is moving right (positive velocity). 
            // This effectively flips the sprite horizontally based on the direction of movement.
            transform.localScale = new Vector2(Mathf.Sign(rb.linearVelocity.x), 1f); 
       
        }
  
    }
    void OnJump(InputValue value)
    {
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
             if(value.isPressed)
             {
             rb.linearVelocity += new Vector2(0f, jumpSpeed);
             }
        }
      
    }

   
}
