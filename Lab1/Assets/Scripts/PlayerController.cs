using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed = 10;
    public float upSpeed;
    private SpriteRenderer marioSprite;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    private bool faceRightState = true;
    private Rigidbody2D marioBody;
    private float moveHorizontal;
    private bool onGroundState = true;
    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
    }

    // FixedUpdate may be called once per frame (see documentation for details)
    void FixedUpdate()
    {
        // Dynamic rigidbody
        moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
            {
                marioBody.AddForce(movement * speed);
            }
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            marioBody.velocity = Vector2.zero; // stop
        }

        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse); // jump
            onGroundState = false;
            marioAnimator.SetBool("onGround", onGroundState);
            countScoreState = true; // check if Goomba is underneath
        }
    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip); // plays sound but without cutting off other sounds
    }

    // Called when Mario's hitbox comes into contact with another gameObject's hitbox
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Goomba!");
            marioAnimator.SetTrigger("onDeath");
            GameObject.FindGameObjectWithTag("Ui").GetComponent<MenuController>().GameOver(score);
        }
    }

    // Called when Mario collides with another gameObject with a collider component
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGroundState = true; // back on ground
            marioAnimator.SetBool("onGround", onGroundState);
            countScoreState = false; // reset score state
            scoreText.text = "Score: " + score.ToString(); // update score
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle faceRightState
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;

            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;

            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        // Update the xSpeed animation parameter to match Mario's horizontal speed
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

        // When jumping and we haven't registered our score
        if (!onGroundState && countScoreState)
        {
            // If Mario jumps over Goomba
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                Debug.Log(score);
            }
        }
    }
}
