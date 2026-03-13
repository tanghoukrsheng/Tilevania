using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 8f; 
    [SerializeField] float jumpSpeed = 12f; 
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathFling = new Vector2(10f, 30f);

    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun; 
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myBoxCollider;

    bool isAlive = true;

    float gravityScaleAtStart;
    // alled once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
       if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();  
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
        if (!isAlive) { return; }


        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
             if(value.isPressed)
             {
             rb.linearVelocity += new Vector2(0f, jumpSpeed);
             }
        }
      
    }

    void ClimbLadder()
    {
        
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
        
        Vector2 climbVelocity = new Vector2(rb.linearVelocity.x, moveInput.y * climbSpeed);
        rb.linearVelocity = climbVelocity;
        rb.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(rb.linearVelocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);

        }
        else
        {
            rb.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
        }

    }

    void Die()
    {
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Mobs", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            rb.linearVelocity = deathFling; 
            FindFirstObjectByType<GameSession>().ProcessPlayerDeath();
        }
    }

    void OnAttack(InputValue value)
    {
        if (!isAlive) { return; }

  
            Instantiate(bullet, gun.position, transform.rotation); // This line creates a new instance of the bullet GameObject at the position of the gun and with the same rotation as the player.
        
    }
   
}
