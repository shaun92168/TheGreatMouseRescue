using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CatController : MonoBehaviour
{
    [Header("General")]
    public Transform player;
    public GameState gameState;

    // alert system variables
    [Header("Alert Settings")]
    public GameObject alertBar;
    public float detectionRange;
    public bool isPlayerInRange;
    public float maxAlertLevel = 100;
    public float alertLevel;
    public float incrementRate = 20;
    public float decreasingRate = 5;
    public float standRate = 1;
    public float runRate = 1.5f;
    public float sneakRate = 0.5f;
    public float crawlRate = 0.2f;
    private float currentRate;

    // SFX
    AudioSource audioSource;
    public AudioClip catMewing;
    public AudioClip catPurring;
    public float volume = 1f;
    private bool playOtherSound = false;
    // set up variables
    void Start()
    {
        alertLevel = 0.0f;
        currentRate = 0.0f;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // detect if player is in alert range
        float currentDistanceFromPlayer = Vector3.Distance(player.position, transform.position);
        if (currentDistanceFromPlayer < detectionRange) isPlayerInRange = true;
        else isPlayerInRange = false;

        // set up alert rate based on player movement
        if (isPlayerInRange)
        {
            switch (gameState.playerState)
            {
                case 0:
                    currentRate = standRate;
                    break;
                case 1:
                    currentRate = runRate;
                    break;
                case 2:
                    currentRate = sneakRate;
                    break;
                case 3:
                    currentRate = crawlRate;
                    break;
            }
        }

        // check if alert is full
        if (alertLevel >= maxAlertLevel)
        {
            gameState.isAlertFull = true;
        }

        // increase or decrease alert base on detecting range
        if (!gameState.isGameOver)
        {
            if (isPlayerInRange)
            {
                alertLevel += incrementRate * currentRate * Time.deltaTime;
                if (!audioSource.isPlaying && !playOtherSound)
                {
                    volume = 0.8f;
                    audioSource.clip = catPurring;
                    audioSource.PlayOneShot(audioSource.clip, volume);
                }
            }
            else if (alertLevel > 0)
            {
                alertLevel -= decreasingRate * Time.deltaTime;
            }
        }

        // update alert bar
        alertBar.GetComponent<Slider>().value = alertLevel / maxAlertLevel;
        if (alertLevel <= 0)
        {
            alertBar.SetActive(false);
        }
        else
        {
            alertBar.SetActive(true);
        }

        Animator anim = GetComponent<Animator>();
        if (alertLevel/maxAlertLevel > 0.5)
        {
            anim.SetBool("sleeping", false);
            playOtherSound = true;
            if (!audioSource.isPlaying && isPlayerInRange)
            {
                audioSource.Stop();
                audioSource.clip = catMewing;
                audioSource.PlayOneShot(audioSource.clip, volume);
            }
        }
        else
        {
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = catMewing;
                audioSource.PlayOneShot(audioSource.clip, volume);
            }
            gameState.isTrapTrigger = true;
            alertLevel = maxAlertLevel;
            SceneManager.LoadScene("GameOver");
        }
    }
}
