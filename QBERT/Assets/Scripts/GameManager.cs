using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum JumpDirection
{
    LeftDown,
    LeftUp,
    RightDown,
    RightUp
}

public class GameManager : Singleton<GameManager> //GameManager talks/inherit to singleton
{

    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI livesUI;

    private Transform RespawnPoint;
    private Transform SpawnPoint;

    private bool isPaused = false;

    public int lives = 3;
    public int score;
    public int enemySpawn = 0;

    public float fallLimit = -7f;

    public GameObject Player;
    public GameObject PlayerPrefab;
    public GameObject coilyPrefab;
    public GameObject redEgg;
    public GameObject UggPrefab;
    public GameObject WrongWayPrefab;
    public GameObject SlickPrefab;
    public GameObject SamPrefab;

    public Color[] cubeColor;
    public List<GameObject> Enemies;

    public bool allCubesChanged = false;

    public Renderer renderer;

    public bool isLoading;

    public override void Awake()
    {
        base.Awake();
        cubeColor = new Color[] { Color.gray, Color.yellow, Color.blue, Color.red };
    }
    // Start is called before the first frame update
    void Start()
    {
        InitScene();
        Enemies = new List<GameObject>();
        InvokeRepeating("AddEnemies", 2.0f, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        UpdateLives();
        FallDetection();
        PauseGame();
    }

    public void AddEnemies()
    {
        GameObject enemySpawns = null;
        if (enemySpawn == 1)
        {
            enemySpawns = coilyPrefab;
        }
        else if (enemySpawn == 3)
        {
            enemySpawns = UggPrefab;
        }
        else if (enemySpawn == 4)
        {
            enemySpawns = WrongWayPrefab;
        }
        else if (enemySpawn == 5)
        {
            enemySpawns = SlickPrefab;
        }
        else if (enemySpawn == 6)
        {
            enemySpawns = SamPrefab;
        }
        else
        {
            enemySpawns = redEgg;
        }
        Enemies.Add(Instantiate(enemySpawns, RespawnPoint.transform.position, transform.rotation));
        enemySpawn++;
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
        isLoading = true;
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        AsyncOperation nextLevel = SceneManager.LoadSceneAsync(currentLevel+1);
        while (!nextLevel.isDone)
        {
            yield return null;
        }
        isLoading = false;
        InitScene();
    }

    private void InitScene()
    {
        SpawnPoint = GameObject.Find("SpawnPoint").transform;
        RespawnPoint = GameObject.Find("RespawnPoint").transform;
        Player = Instantiate(PlayerPrefab, SpawnPoint.position, SpawnPoint.rotation);
        enemySpawn = 0;
    }

    public void UpdateScore()
    {
       scoreUI.text = "Score: " + score.ToString();
    }

    public void UpdateLives()
    {
        livesUI.text = "Lives: " + lives.ToString();
    }

    public void FallDetection()
    {
        if (!isLoading)
        {
            if (Player.transform.position.y < fallLimit)
            {
                Respawn();
            }
        }
      
    }


    public void Respawn()
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
