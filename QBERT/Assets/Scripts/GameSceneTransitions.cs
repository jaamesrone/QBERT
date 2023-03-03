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
        playButton.onClick.AddListener(LoadQbertGame);
      
    }


    public void LoadQbertGame()
    {

        SceneManager.LoadScene(2);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        GameManager.Instance.TogglePause();
    }

    /*
    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToControls()
    {
        SceneManager.LoadScene(1);
    }
    */
}
