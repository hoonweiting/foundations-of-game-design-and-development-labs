using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // private float originalX; // instantiate a float variable called originalX, without assigning a value
    // private float previousX; // instantiate a float variable called previousX, without assigning a value
    // private float maxOffset = 7.5f; // instantiate a float variable called maxOffset, giving it a value of 5.12
    // private float distanceFromWall; // instantiate a float variable called distanceFromWall, without assigning a value
    // private float distanceToTravel; // instantiate a float variable called distanceToTravel, without assigning a value
    private float enemySpeed = 2.5f; // instantiate a float variable called enemyPatrolTime, giving it a value of 2.5
    private int moveRight = -1; // instantiate an integer variable called moveRight, giving it a value of -1
    private Vector2 velocity; // instantiate a Vector2 object called velocity, without assigning a value
    private Rigidbody2D enemyBody; // instantiate a Rigidbody2D object called enemyBody, without assigning a value

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        velocity = new Vector2(moveRight * enemySpeed, 0);
        // originalX = transform.position.x; // get the starting position of the enemy
        // previousX = originalX;
        // ComputeTravelDistance();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("LevelBounds"))
        {
            moveRight *= -1;
            velocity = new Vector2(moveRight * enemySpeed, 0); // change direction
        }
    }

    // void ComputeTravelDistance()
    // {
    //     distanceFromWall = maxOffset - (moveRight * transform.position.x);
    //     distanceToTravel = Random.Range(1.0f, distanceFromWall);
    // }

    void MoveGoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        MoveGoomba();
        // if (Mathf.Abs(enemyBody.position.x - previousX) < distanceToTravel)
        // {
        //     MoveGoomba(); // move goomba
        // }
        // else
        // {
        //     moveRight *= -1; // change direction
        //     previousX = enemyBody.position.x; // update the enemy's last "checkpoint"
        //     ComputeTravelDistance();
        //     ComputeVelocity();
        //     MoveGoomba();
        // }
    }
}
