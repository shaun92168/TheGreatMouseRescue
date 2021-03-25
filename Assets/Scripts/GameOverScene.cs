using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void BacktoStory()
    {
        SceneManager.LoadScene("StoryScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
