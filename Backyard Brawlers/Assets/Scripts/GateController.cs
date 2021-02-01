/*
 * Backyard Brawlers
 * CS 3410
 * December 1, 2020
 * Jordan Lanius - jwlq89@umsystem.edu
 * 
 * - Gate Controller -
 * This script handles the opening and closing of the gates between yards.
 * It is based off the one included with the asset. (DoorController.cs)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class GateController : MonoBehaviour
{
    // Gate state enumerator
    public enum GateState
    {
        Open,
        Closed
    }

    // Public getter/setter for currentState
    public GateState CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
            Animate();
            SetCollider();
        }
    }

    // Public bool getters for currentState
    public bool isGateOpen { get { return CurrentState == GateState.Open; } }
    public bool isGateClosed { get { return CurrentState == GateState.Closed; } }

    public float speed = 1;
    public GateState initialState = GateState.Closed;

    [SerializeField]
    private AnimationClip openAnimation;
    [SerializeField]
    private AnimationClip closeAnimation;
    [SerializeField]
    private BoxCollider openCollider;
    [SerializeField]
    private BoxCollider closedCollider;

    private Animation animator;
    private GateState currentState;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animation>();
        animator.playAutomatically = false;

        openAnimation.legacy = true;
        closeAnimation.legacy = true;
        animator.AddClip(openAnimation, GateState.Open.ToString());
        animator.AddClip(closeAnimation, GateState.Closed.ToString());

        currentState = initialState;
        var clip = GetCurrentAnimation();
        animator[clip].speed = 9999;
        animator.Play(clip);

        SetCollider();
    }

    // Open the gate
    public void OpenGate()
    {
        if (isGateClosed)
        {
            CurrentState = GateState.Open;
        }
    }

    // Close the gate
    public void CloseGate()
    {
        if (isGateOpen)
        {
            CurrentState = GateState.Closed;
        }
    }

    // Toggle the gate state
    public void ToggleGate()
    {
        if (isGateOpen)
        {
            CloseGate();
        }
        else
        { 
            OpenGate();
        }
    }

    // Animate the gate
    private void Animate()
    {
        string clip = GetCurrentAnimation();
        animator[clip].speed = speed;
        animator.Play(clip);
    }

    // Enable collider that matches currentState
    private void SetCollider()
    {
        if (isGateOpen)
        {
            openCollider.enabled = true;
            closedCollider.enabled = false;
        } else
        {
            openCollider.enabled = false;
            closedCollider.enabled = true;
        }
    }

    // Get the current state in string form
    private string GetCurrentAnimation()
    {
        return CurrentState.ToString();
    }
}
