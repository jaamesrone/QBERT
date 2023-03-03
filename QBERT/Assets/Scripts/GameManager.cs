using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager> //GameManager talks/inherit to singleton
{
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI livesUI;

    public int lives = 3;
    public int score;

    public float fallLimit = -7f;

    private Transform RespawnPoint;
    private Transform SpawnPoint;
    public GameObject Player;
    public GameObject PlayerPrefab;

    public Color colorToChangeTo;

    public bool allCubesChanged = false;

    public Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        colorToChangeTo = Color.yellow;
        InitScene();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        UpdateLives();
        FallDetection();
    }

    public void CheckAllCubesChangedColor()
    {
        // Find all the cubes in the scene
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");

        // Check if all the cubes have been changed to the target color
        foreach (GameObject cube in cubes)
        {
            Renderer cubeRenderer = cube.GetComponent<Renderer>();
            if (cubeRenderer.material.color != colorToChangeTo)
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
        StartCoroutine("LoadSceneCo");
    }

    private IEnumerator LoadSceneCo()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        AsyncOperation newScene = SceneManager.LoadSceneAsync(currentSceneIndex+1);
        while (!newScene.isDone)
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
        scoreUI.text = "Score: " + score.ToString();
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
}
