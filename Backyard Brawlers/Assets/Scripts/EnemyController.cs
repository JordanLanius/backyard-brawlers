/*
 * Backyard Brawlers
 * CS 3410
 * December 1, 2020
 * Jordan Lanius - jwlq89@umsystem.edu
 * 
 * - Enemy Controller -
 * This script handles the movement and actions of enemies.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameController gameManager;      // Main game controller
    public GameObject player;               // Player game object
    public Animator animator;               // Enemy animator
    public float speed;                     // Speed of player
    public GameObject rightTrigger;
    public GameObject leftTrigger;
    public bool isActive = false;
    public bool isFainted = false;

    private Rigidbody rb;
    private AudioSource[] sounds;
    private Vector3 initialPosition;
    private int health = 3;
    private float punchTimer = 0f;
    private float decisionTimer = 0f;
    private int decision = 5;
    private float animSpeed = 0f;
    private float direction = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        sounds = GetComponents<AudioSource>();
        initialPosition = transform.position;
        rightTrigger.SetActive(false);
        leftTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isPaused && !gameManager.isFinished && isActive)
        {
            // Always have enemy facing player
            if (player.transform.position.x <= transform.position.x)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
            animator.SetFloat("Direction", direction);

            // Decide what action to perform
            if (decisionTimer >= 1f)
            {
                decision = Random.Range(0, 6);
                decisionTimer = 0f;
            }

            punchTimer += Time.deltaTime;
            decisionTimer += Time.deltaTime;
        }
    }

    // FixedUpdate is called once per frame in sync with the physics engine
    void FixedUpdate()
    {
        if (!gameManager.isPaused && !gameManager.isFinished && isActive)
        {
            Vector3 movement = transform.position;
            
            switch (decision)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    if (direction == -1) {
                        movement = new Vector3((player.transform.position.x + 1), 0f, player.transform.position.z) - transform.position;
                    }
                    else
                    {
                        movement = new Vector3((player.transform.position.x - 1), 0f, player.transform.position.z) - transform.position;
                    }
                    break;
                case 4:
                    movement = initialPosition - transform.position;
                    break;
                default:
                    break;
            }

            if (movement.magnitude > 1)
            {
                if (punchTimer > 0.2f)
                {
                    rb.AddForce(movement * speed * Time.deltaTime);
                }
                else
                {
                    rb.AddForce(movement * (speed / 10) * Time.deltaTime);
                }
            }
            else
            {
                rb.velocity = Vector3.zero;
            }

            animSpeed = rb.velocity.magnitude * speed;
            animator.SetFloat("Speed", Mathf.Abs(animSpeed));
        }
    }


    // Collision handling
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerPunch"))
        {
            Hit();
        }
    }

    // Enemy punches
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

    // Enemy is hit
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

    // Enemy faints
    private void Faint()
    {
        animator.SetBool("Faint", true);

        isFainted = true;

        rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
