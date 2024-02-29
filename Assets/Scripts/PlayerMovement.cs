using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    public bool isJumping; // a bool means is it a true or false statement
    public float playersMove;
    public Rigidbody2D rb;
    // Start is called before the first frame update

    GameManager gameManager;
    Vector2 startPosition;

    void Start()
    {
        // The rigid body 2d allows an object to have simulating physical interactions
        rb = GetComponent<Rigidbody2D>();

        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameController.GetComponent<GameManager>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    public void Update()
    {
        // this gets the arrow keys or Awsd keys to move the character left or right
        playersMove = Input.GetAxis("Horizontal");

        // this moves the character left or right depending on the input speed
        //rb.velocity = new Vector2(speed * gameManager.moveSpeed, rb.velocity.y);

        rb.velocity = new Vector2(speed * gameManager.moveVector.x, rb.velocity.y);

        // if the charcter is not jumping and the player is pressing the jump button
        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            // this allows the character to jump
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            Debug.Log("Jump"); // example in how to leave debug statements
        }

        if (gameManager.GetResetPlayer())
        {
            Debug.Log("Reset Player");
            transform.position = startPosition;
            //rb.gravityScale = 0;
            gameManager.SetResetPlayer(false);

            //transform.vel
        }


    }

    // this function checks if the user is on the ground if so then the charcter isnt jumping
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    // this function checks if the user is off the ground if so then the charcter is jumping
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }
}

