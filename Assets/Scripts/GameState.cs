using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "GameState")]
public class GameState : ScriptableObject
{
    [Header("Game State")]
    public bool isAlertFull;
    public bool isTrapTrigger;
    public bool isGameOver;

    [Header("Alert System")]
    public float nearestKittenDistance;
    public bool isAlertIncreasing;
    public float alertLevel;
    public float maxAlertLevel;

    [Header("Player Status")]
    // 0 = Stand, 1 = Run , 2 = Sneak, 3 = Crawl
    public int playerState;
}
