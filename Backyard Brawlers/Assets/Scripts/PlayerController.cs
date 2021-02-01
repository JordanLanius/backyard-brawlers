/*
 * Backyard Brawlers
 * CS 3410
 * December 1, 2020
 * Jordan Lanius - jwlq89@umsystem.edu
 * 
 * - Player Controller -
 * This script handles the movement and actions of the player.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameController gameManager;      // Main game controller
    public Animator animator;               // Player animator
    public float speed;                     // Speed of player
    public GameObject rightTrigger;
    public GameObject leftTrigger;

    private Rigidbody rb;
    private AudioSource[] sounds;
    private int health = 3;
    private float punchTimer = 0f;
    private float animSpeed = 0f;
    private float direction = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        sounds = GetComponents<AudioSource>();
        rightTrigger.SetActive(false);
        leftTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= -88.4)
        {
            gameManager.EndGame(true);
        }

        if (!gameManager.isPaused && !gameManager.isFinished)
        {
            if (punchTimer > 0.3f)
            {
                rightTrigger.SetActive(false);
                leftTrigger.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Space) && (punchTimer > 0.5f))
            {
                Punch();
            }

            punchTimer += Time.deltaTime;
        }
    }

    // FixedUpdate is called once per frame in sync with the physics engine
    void FixedUpdate()
    {
        if (!gameManager.isPaused && !gameManager.isFinished)
        {
            // Get input
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Set direction
            if (moveHorizontal > 0)
            {
                direction = 1f;
            }
            else if (moveHorizontal < 0)
            {
                direction = -1f;
            }
            animator.SetFloat("Direction", direction);

            // Move based on input
            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

            if (punchTimer > 0.2f)
            {
                animSpeed = movement.magnitude * speed;
                animator.SetFloat("Speed", Mathf.Abs(animSpeed));

                rb.velocity = movement * speed;
            }
            else
            {
                rb.velocity = movement * speed / 10;
                animator.SetFloat("Speed", 0);
            }
        }
    }

    // Collision handling
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyPunch"))
        {
            Hit();
        }
    }

    // Player punches
    private void Punch()
    {
        punchTimer = 0;

        animator.SetTrigger("Punch");
        sounds[0].Play();

        if (direction > 0)
        {
            rightTrigger.SetActive(true);
        }
        else
        {
            leftTrigger.SetActive(true);
        }
    }

    // Player is hit
    private void Hit()
    {
        health--;

        if (health > 0)
        {
            animator.SetTrigger("Hit");

            punchTimer = 0;
        }
        else
        {
            Faint();
        }
    }

    // Player faints
    private void Faint()
    {
        animator.SetTrigger("Faint");

        gameManager.EndGame(false);
    }
}
