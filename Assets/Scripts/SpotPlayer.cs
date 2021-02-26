using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpotPlayer : MonoBehaviour
{
    [SerializeField]
    private Material myMaterial;
    public Color SpotColor;
    public Transform player;
    public float detectionRange;
    public bool isPlayerInRange;

//    private Color originalColor;
    public Text label;
    public GameState gameState;

    public int tempCounter = 0;

    void Awake()
    {
        label.text = "";
        myMaterial.color = SpotColor;
    }

    void Update()
    {
        float currentDistanceFromPlayer = Vector3.Distance(player.position, transform.position);
        if (currentDistanceFromPlayer < detectionRange) isPlayerInRange = true;
        else isPlayerInRange = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            label.text = "!!!";
            myMaterial.color = Color.red;
            tempCounter += 1;
            gameState.isTrapTrigger = true;
            gameState.alertLevel = gameState.maxAlertLevel;
            if (tempCounter == 5)
            {
               SceneManager.LoadScene("GameOver");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            label.text = "";
            myMaterial.color = SpotColor;
        }
        myMaterial.color = SpotColor;
    }

}
