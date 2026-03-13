using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{

  [SerializeField] AudioClip coinSoundEffect;
  bool isCollected = false; // flag to check if the coin has been collected to prevent multiple collections and sound effects from playing if the player collides with the coin multiple times before it is destroyed.
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCollected)
        {   
            isCollected = true;
            AudioSource.PlayClipAtPoint(coinSoundEffect, transform.position); // Play the sound effect
            Destroy(gameObject); 
            
        }
    }
}
