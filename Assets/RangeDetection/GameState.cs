using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "GameState")]
public class GameState : ScriptableObject
{
    public float nearestKittenDistance;

    public float alertLevel;
    public float maxAlertLevel;

    public bool isAlertFull;
    public bool isTrapTrigger;
    public bool isGameOver;
}
