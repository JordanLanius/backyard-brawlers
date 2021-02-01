/*
 * Backyard Brawlers
 * CS 3410
 * December 1, 2020
 * Jordan Lanius - jwlq89@umsystem.edu
 * 
 * - Yard Controller -
 * This script handles each yard separately, opening the gate when conditions are met.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YardController : MonoBehaviour
{
    public enum ClearCondition
    {
        Enemies,
        Pickup,
        Free
    }

    public GameController gameManager;   // Main game controller
    public GameObject player;
    public GameObject gate;                 // TODO fix gate toggle
    public ClearCondition clearCondition;
    public EnemyController[] enemies;

    private bool isFinished = false;
    private GateController gateController;
    private float delay = 0;

    // Start is called before the first frame update
    void Start()
    {
        gateController = gate.GetComponent<GateController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinished && !gameManager.isPaused)
        {
            if ((clearCondition == ClearCondition.Free) && (delay > 1.0f))
            {
                // Open the gate after a set delay
                Finish();
            }
            else if (clearCondition == ClearCondition.Enemies)
            {
                // Open the gate if all enemies defeated
                bool test = true;

                foreach (var enemy in enemies)
                {
                    if (!enemy.isFainted)
                    {
                        test = false;
                    }
                }

                if (test)
                {
                    Finish();
                }
            }
            else
            {
                delay += Time.deltaTime;
            }
        }
    }

    // Open the gate when the conditions are met
    void Finish()
    {
        isFinished = true;
        gateController.OpenGate();
    }

    // Collision handling
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (var enemy in enemies)
            {
                enemy.isActive = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (var enemy in enemies)
            {
                enemy.isActive = false;
            }
        }
    }
}
