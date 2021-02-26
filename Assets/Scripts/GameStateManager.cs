using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameState gameState;
    public List<GameObject> traps;

    private string alertingTrap;

    void Start()
    {
        gameState.alertLevel = 0;
        gameState.isTrapTrigger = false;
        gameState.isAlertFull = false;
        gameState.isGameOver = false;
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
        }

        foreach (GameObject trap in traps)
        {
            if (trap.GetComponent<SpotPlayer>().isPlayerInRange)
            {
                gameState.isAlertIncreasing = true;
                alertingTrap = trap.name;
                Debug.Log("Alerting " + alertingTrap);
                break;
            }
            else gameState.isAlertIncreasing = false;

        }
    }
}
