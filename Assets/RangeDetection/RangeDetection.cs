using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetection : MonoBehaviour
{
    public List<Transform> kittens;
    public Transform player;
    public GameState gameState;

    private float shortestDistance;
    void Update()
    {
        shortestDistance = 100;
        foreach (Transform kitten in kittens)
        {
            float dist = Vector3.Distance(kitten.position, player.position);
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                gameState.nearestKittenDistance = shortestDistance;
            }
        }
    }
}
