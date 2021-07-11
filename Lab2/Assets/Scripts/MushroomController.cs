using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    private bool stationary = false;
    private Vector2 previousVelocity;
    private BoxCollider2D mushroomBox;
    private Rigidbody2D mushroomBody;

    // Start is called before the first frame update
    void Start()
    {
        mushroomBox = GetComponent<BoxCollider2D>();
        mushroomBox.sharedMaterial.friction = 0;
        mushroomBox.enabled = false;
        mushroomBox.enabled = true;
        mushroomBody = GetComponent<Rigidbody2D>();
        mushroomBody.AddForce(Vector2.one * 3, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        previousVelocity = mushroomBody.velocity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!stationary)
        {
            if (col.gameObject.CompareTag("Obstacle") || col.gameObject.CompareTag("LevelBounds"))
            {
                mushroomBody.velocity = new Vector2(-1 * previousVelocity.x, previousVelocity.y); // change direction
            }
            else if (col.gameObject.CompareTag("Player"))
            {
                stationary = true; // stop moving
                mushroomBox.sharedMaterial.friction = 1;
                mushroomBox.enabled = false;
                mushroomBox.enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
