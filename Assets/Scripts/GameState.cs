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
    public bool level1Complete;
    public bool level2Complete;
    public bool level3Complete;
    public int activeLevel;

    [Header("Alert System")]
    public float nearestKittenDistance;
    public bool isAlertIncreasing;
    public float alertLevel;
    public float maxAlertLevel;

    [Header("Player Status")]
    // 0 = Stand, 1 = Run , 2 = Sneak, 3 = Crawl
    public int playerState;

}
