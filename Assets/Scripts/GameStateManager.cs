using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public GameState gameState;
    public List<GameObject> traps;
    public GameObject mainCam;
    public GameObject climbCam;

    private string alertingTrap;

    void Start()
    {
        gameState.alertLevel = 0;
        gameState.isTrapTrigger = false;
        gameState.isAlertFull = false;
        gameState.isGameOver = false;
        gameState.level1Complete = false;
        gameState.level2Complete = false;
        gameState.level3Complete = false;
        gameState.activeLevel = 1;
        mainCam.SetActive(true);
        mainCam.GetComponent<AudioListener>().enabled = true;
        climbCam.SetActive(false);
        climbCam.GetComponent<AudioListener>().enabled = false;

    }

    void Update()
    {
        if(gameState.isTrapTrigger)
        {
            gameState.isGameOver = true;
        }
        if(gameState.isAlertFull)
        {
            gameState.isGameOver = true;
        }
        if(gameState.isGameOver)
        {
            //trigger cat, play animation
            SceneManager.LoadScene("GameOver");
        }

        // escape to quit the game
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("GameOver");
        }

        if (gameState.level1Complete)
        {
            gameState.activeLevel = 2;
            SceneManager.LoadScene("CutScene_2");
            gameState.level1Complete = false;
        }

        if (gameState.level2Complete)
        {
            gameState.activeLevel = 3;
            SceneManager.LoadScene("CutScene_3");
            gameState.level2Complete = false;
        }

        if (gameState.level3Complete)
        {
            gameState.activeLevel = 4;
            SceneManager.LoadScene("StoryScene_Act3");
            gameState.level3Complete = false;
        }
    }
}
