using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeColorChange : MonoBehaviour
{
    private Color originalColor;
    private Renderer renderer;
    private bool allCubesChanged = false;

    private void Start()
    {
        // Get reference to the renderer of the cube
        renderer = GetComponent<Renderer>();
    }


    void Update()
    {
        // Check if all the cubes have been changed
        // and if so, make the debug log statement
        if (allCubesChanged == true)
        {
            Debug.Log("Completed!");
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        // If the player collides with the cube, change the color of the cube
        if (other.gameObject.CompareTag("Player"))
        {
            renderer.material.color = GameManager.Instance.colorToChangeTo;
            GameManager.Instance.CheckAllCubesChangedColor();
        
        }
    }

}