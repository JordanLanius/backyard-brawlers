/*
 * Backyard Brawlers
 * CS 3410
 * December 1, 2020
 * Jordan Lanius - jwlq89@umsystem.edu
 * 
 * - Game Controller -
 * This script handles the UI and end game functions.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject finishMenu;
    public Text finishText;
    public Button pauseButton;
    public Button muteButton;

    public Sprite pauseSprite;
    public Sprite unpauseSprite;
    public Sprite muteSprite;
    public Sprite unmuteSprite;

    public bool isMuted = false;
    public bool isPaused = true;
    public bool isFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                unpauseGame();
            else
                pauseGame();
        }
    }

    // Ends the game in victory/defeat
    public void EndGame(bool victory)
    {
        isFinished = true;
        isPaused = true;

        if (victory)
        {
            finishText.text = "You Win";
        }
        else
        {
            finishText.text = "Game Over";
        }

        finishMenu.SetActive(true);
    }

    // Pause the game
    void pauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        pauseButton.GetComponent<Image>().sprite = unpauseSprite;
    }

    // Unpause the game
    void unpauseGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        pauseButton.GetComponent<Image>().sprite = pauseSprite;
    }

    // Start game
    public void OnStartButtonPress()
    {
        startMenu.SetActive(false);
        isPaused = false;
        settingsMenu.SetActive(true);
    }

    // Mute sound
    public void OnMuteButtonPress()
    {

    }

    // Open pause menu
    public void OnPauseButtonPress()
    {
        if (isPaused)
            unpauseGame();
        else
            pauseGame();
    }

    // Close pause menu
    public void OnResumeButtonPress()
    {
        unpauseGame();
    }

    // Restart the game
    public void OnRestartButtonPress()
    {
        SceneManager.LoadScene("First Street");
    }

    // Quit the game
    public void OnQuitButtonPress()
    {
        Application.Quit();
    }
}
