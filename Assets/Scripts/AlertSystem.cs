using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertSystem : MonoBehaviour
{
    public GameState gameState;
    public float detectingRange;
    public float incrementRate;
    public float decreasingRate;

    void Update()
    {
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
                gameState.alertLevel += incrementRate * Time.deltaTime;
            }
            else if(gameState.alertLevel > 0)
            {
                gameState.alertLevel -= decreasingRate * Time.deltaTime;
            }
        }
    }
}
