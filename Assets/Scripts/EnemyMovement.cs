using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        rb.linearVelocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision) 
    {
        moveSpeed = -moveSpeed;
        FlipMobSprite();
    }

    void FlipMobSprite()
    {
        transform.localScale = new Vector2(-Mathf.Sign(rb.linearVelocity.x), 1f); 
    }
}
