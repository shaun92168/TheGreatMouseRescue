using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertSystem : MonoBehaviour
{
    public GameState gameState;
    public float detectingRange;
    public float incrementRate;
    public float decreasingRate;

    // Start is called before the first frame update
    void Start()
    {
        gameState.alertLevel = 0;
        gameState.isAlertFull = false;
        gameState.isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState.alertLevel >= gameState.maxAlertLevel)
        {
            gameState.isAlertFull = true;
            gameState.isGameOver = true;
        }

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
