/*
 * Backyard Brawlers
 * CS 3410
 * December 1, 2020
 * Jordan Lanius - jwlq89@umsystem.edu
 * 
 * - Camera Controller -
 * This script handles the movement of the camera.
 * It remains centered on the x-axis of the player unless at boundary.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;   // Player gameobject that the camera is centered on

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, 15, -25);
    }

    // LateUpdate is alled once per frame after all other Update methods are processed
    void LateUpdate()
    {
        // Prevent camera from leaving boundary
        if (player.transform.position.x < -222)
        {
            transform.position = new Vector3(-222, 15, -25);
        }
        else if (player.transform.position.x > 222)
        {
            transform.position = new Vector3(222, 15, -25);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, 15, -25);
        }
    }
}
