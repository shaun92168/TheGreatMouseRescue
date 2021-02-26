using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertSystem : MonoBehaviour
{
    public GameState gameState;
    public float detectingRange;
    public float incrementRate;
    public float decreasingRate;

    public float standRate = 1;
    public float runRate = 1.5f;
    public float sneakRate = 0.5f;
    public float crawlRate = 0.2f;

    private float currentMovementRate;

    void Update()
    {
        // determin movement rate
        switch(gameState.playerState)
        {
            case 0:
                currentMovementRate = standRate;
                break;
            case 1:
                currentMovementRate = runRate;
                break;
            case 2:
                currentMovementRate = sneakRate;
                break;
            case 3:
                currentMovementRate = crawlRate;
                break;
        }

        // check if alert is full
        if(gameState.alertLevel >= gameState.maxAlertLevel)
        {
            gameState.isAlertFull = true;
        }

        // increase or decrease alert base on detecting range
        if(!gameState.isGameOver)
        {
            if(gameState.nearestKittenDistance <= detectingRange)
            {
                gameState.alertLevel += incrementRate * currentMovementRate * Time.deltaTime;
            }
            else if(gameState.alertLevel > 0)
            {
                gameState.alertLevel -= decreasingRate * Time.deltaTime;
            }
        }
    }
}
