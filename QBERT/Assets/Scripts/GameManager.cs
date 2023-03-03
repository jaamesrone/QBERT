using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager> //GameManager talks/inherit to singleton
{
    private Transform RespawnPoint;
    private Transform SpawnPoint;

    private bool isPaused = false;

    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI livesUI;

    public int lives = 3;
    public int score;

    public float fallLimit = -7f;

    
    public GameObject Player;
    public GameObject PlayerPrefab;

    //public Color colorToChangeTo;
    //public Color SecondColorToChangeTo;

    public Color[] cubeColor;

    public bool allCubesChanged = false;

    public Renderer renderer;

    public override void Awake()
    {
        base.Awake();
        cubeColor = new Color[] { Color.gray, Color.yellow, Color.blue, Color.red };
    }
    // Start is called before the first frame update
    void Start()
    {
        InitScene();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        UpdateLives();
        FallDetection();
        PauseGame();
    }

    public void CheckAllCubesChangedColor()
    {
        // Find all the cubes in the scene
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");

        // Check if all the cubes have been changed to the target color
        foreach (GameObject cube in cubes)
        {
            CubeColorChange ColorChanger = cube.GetComponent<CubeColorChange>();
            if (!ColorChanger.targetColor)
            {
                return;
            }
        }

        // All the cubes have been changed to the target color
        allCubesChanged = true;
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        StartCoroutine("LoadToNexeLevel");
    }

    private IEnumerator LoadToNexeLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        AsyncOperation nextLevel = SceneManager.LoadSceneAsync(currentLevel+1);
        while (!nextLevel.isDone)
        {
            yield return null;
        }
        InitScene();
    }

    private void InitScene()
    {
        SpawnPoint = GameObject.Find("SpawnPoint").transform;
        RespawnPoint = GameObject.Find("RespawnPoint").transform;
        Player = Instantiate(PlayerPrefab, SpawnPoint.position, SpawnPoint.rotation);
    }

    public void UpdateScore()
    {
//        scoreUI.text = "Score: " + score.ToString();
    }

    public void UpdateLives()
    {
        livesUI.text = "Lives: " + lives.ToString();
    }

    public void FallDetection()
    {
        if (Player.transform.position.y < fallLimit)
        {
            Respawn();
        }
    }


    private void Respawn()
    {
        Player.transform.position = RespawnPoint.position;
        lives--;
        // Optionally, you could also reset any other necessary player state here
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        }
        else
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("PauseMenu");
        }
    }
}
