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
    private Scene currentScene;

    void Start()
    {
        gameState.alertLevel = 0;
        gameState.isTrapTrigger = false;
        gameState.isAlertFull = false;
        gameState.isGameOver = false;
        mainCam.SetActive(true);
        mainCam.GetComponent<AudioListener>().enabled = true;
        climbCam.SetActive(false);
        climbCam.GetComponent<AudioListener>().enabled = false;
        currentScene = SceneManager.GetActiveScene();

        switch (currentScene.name)
        {
            case "GamePlay_1":
                gameState.activeLevel = 1;
                gameState.level1Complete = false;
                gameState.level2Complete = false;
                gameState.level3Complete = false;
                break;
            case "GamePlay_2":
                gameState.activeLevel = 2;
                gameState.level1Complete = true;
                gameState.level2Complete = false;
                gameState.level3Complete = false;
                break;
            case "GamePlay_3":
                gameState.activeLevel = 3;
                gameState.level1Complete = true;
                gameState.level2Complete = true;
                gameState.level3Complete = false;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (gameState.isTrapTrigger)
        {
            gameState.isGameOver = true;
        }
        if (gameState.isAlertFull)
        {
            gameState.isGameOver = true;
        }
        if (gameState.isGameOver)
        {
            //trigger cat, play animation
            StartCoroutine(deathWaitCoroutine());
        }

        // escape to quit the game
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    IEnumerator deathWaitCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameOver");
    }
}
