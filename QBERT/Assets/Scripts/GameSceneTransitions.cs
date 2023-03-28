using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameSceneTransitions : MonoBehaviour
{
    public Button playButton;

    private void Start()
    {
        if (playButton!=null)
        {
            playButton.onClick.AddListener(LoadQbertGame);
        }

    }

    public void LoadQbertGame()
    {

        SceneManager.LoadScene(3);

    }

    public void restart() //restarting gaming
    {
        GameManager.Instance.score = 0;
        GameManager.Instance.rounds = 1;
        GameManager.Instance.lives = 3;
        GameManager.Instance.LoadNextLevel(3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        GameManager.Instance.TogglePause();
    }
}
