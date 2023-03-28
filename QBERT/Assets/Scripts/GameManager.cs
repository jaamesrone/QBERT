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
    public TextMeshProUGUI roundsUI;
    public TextMeshProUGUI livesUI;

    public Transform RespawnPoint;
    private Transform SpawnPoint;

    public int rounds = 1;
    public int lives = 3;
    public int score;
    public int enemySpawn = 0;

    public float fallLimit = -7f;

    public GameObject CoilyPrefab;
    public GameObject Player;
    public GameObject PlayerPrefab;
    public GameObject purpleEggPrefab;
    public GameObject redEgg;
    public GameObject UggPrefab;
    public GameObject WrongWayPrefab;
    public GameObject SlickPrefab;
    public GameObject SamPrefab;
    public GameObject GreenEgg;

    public Color[] cubeColor;
    public List<GameObject> Enemies;

    public Renderer renderer;

    public bool isLoading;
    private bool isPaused = false;
    public bool allCubesChanged = false;

    public GameObject canvas;

    public override void Awake()
    {
        base.Awake();
        cubeColor = new Color[] { Color.gray, Color.yellow, Color.blue, Color.red };
    }
    // Start is called before the first frame update
    void Start()
    {
        thisScene();
        Enemies = new List<GameObject>();
        InvokeRepeating("AddEnemies", 2.0f, 2.5f); //2.5f or 2.8 or 3
    }

    public override void DestroyObjecet()
    {
        Destroy(canvas);
        Destroy(gameObject);
    }

    public override void DontDestroyObject()
    {
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        UpdateRounds();
        UpdateLives();
        FallDetection();
        PauseGame();
    }

    public void AddEnemies()//a list of gameobjects objects being assigned. then they get instantiated when enemySpawn increases. 
    {
        GameObject enemySpawns = null;
    
        
        if (enemySpawn == 1) 
        {
            enemySpawns = GreenEgg;
        }
        else if (enemySpawn == 2)
        {
            enemySpawns = purpleEggPrefab;
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

        if (enemySpawns==UggPrefab )
        {
            Instantiate(UggPrefab, new Vector3(1, -6, 5), transform.rotation);
        }
        else if (enemySpawns == WrongWayPrefab)
        {
            Instantiate(WrongWayPrefab, new Vector3(5, -6, 1), transform.rotation);
        }
        else
        {
            Enemies.Add(Instantiate(enemySpawns, RespawnPoint.transform.position, transform.rotation));
        }
        enemySpawn++;
        if (enemySpawn >= 7)
        {
            enemySpawn = 0;
            
        }
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
        LoadNextLevel(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void LoadNextLevel(int sceneNum) //loads to the next level and if parameter is > 5 restart!
    {
        if (sceneNum>5)
        {
            sceneNum = 1;
        }
        StartCoroutine(LoadToNextLevel(sceneNum));
    }

    private IEnumerator LoadToNextLevel(int sceneNum) //loads to the next level and finds gameobjects to get assigned.
    {
        isLoading = true;
        AsyncOperation nextLevel = SceneManager.LoadSceneAsync(sceneNum);
        while (!nextLevel.isDone)
        {
            yield return null;
        }
        isLoading = false;
        thisScene();
        rounds++;

    }

    public void thisScene() //finds gameobjects to assign themself, due to them destroying. 
    {
        if (SceneManager.GetActiveScene().buildIndex>=3&&SceneManager.GetActiveScene().buildIndex<=5)
        {
            SpawnPoint = GameObject.Find("SpawnPoint").transform;
            RespawnPoint = GameObject.Find("RespawnPoint").transform;
            Player = Instantiate(PlayerPrefab, SpawnPoint.position, SpawnPoint.rotation);
        }

        enemySpawn = 0; //restarts enemies 
    }

    public void UpdateScore()
    {
       scoreUI.text = "Score: " + score.ToString();
    }

    public void UpdateRounds()
    {
        roundsUI.text = "Rounds: " + rounds.ToString();
    }

    public void UpdateLives()
    {
        livesUI.text = "Lives: " + lives.ToString();
    }

    public void FallDetection()
    {
        if (!isLoading)
        {
            if (Player!=null&&Player.transform.position.y < fallLimit)
            {
                Respawn();
            }
        }
      
    }


    public void Respawn()
    {
        Player.transform.position = RespawnPoint.position;
        lives--;
        if (lives <= 0)
        {
            Player.SetActive(false);
            GameOver();
        }
        
    }


    public void GameOver()
    {
        SceneManager.LoadScene(1);
    }

    public void PauseGame() //pres esc to pause the game.
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    
    public void TogglePause()//sets time to 1 to freeze time. 
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