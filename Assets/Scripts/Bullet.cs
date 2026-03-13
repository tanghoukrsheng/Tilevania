using UnityEngine;

public class Bullet : MonoBehaviour
{
  
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D rb;

    PlayerMovement player;
    float xSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerMovement>();
        xSpeed = player.transform.localScale.x;
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(bulletSpeed * xSpeed, 0f); 
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
       if (collision.CompareTag("Goober"))
        {
            Destroy(collision.gameObject); 
            Destroy(gameObject); 
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
            Destroy(gameObject, 1f); 
        
    }


    } 

