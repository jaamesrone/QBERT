using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeColorChange : MonoBehaviour
{
    private Renderer renderer;
    private bool hasBeenSteppedOn = false;
    public int currentCubeColor = 0;
    public int maxColorIndex;
    public bool targetColor;

    private void Start()
    {
        // Get reference to the renderer of the cube
        renderer = GetComponent<Renderer>();
        renderer.material.color = GameManager.Instance.cubeColor[currentCubeColor];
    }

    void Update()
    {

    }

private void OnCollisionEnter(Collision other)
    {
        // If the player collides with the cube, change the color of the cube
        if (other.gameObject.CompareTag("Player")) //oncollision with qbert touching to cube. cube changes color.  
        {
            if (!hasBeenSteppedOn)
            {
                GameManager.Instance.score += 25;
                GameManager.Instance.UpdateScore();
                hasBeenSteppedOn = true;

            }
            currentCubeColor++;
            if (currentCubeColor > maxColorIndex)
            {
                return;
            }

            else
            {
                if (currentCubeColor == maxColorIndex)
                {
                    targetColor = true;
                }
                renderer.material.color = GameManager.Instance.cubeColor[currentCubeColor];
                GameManager.Instance.CheckAllCubesChangedColor();
            }
        }
        else if (other.gameObject.tag == "Slick" || other.gameObject.tag == "Sam")
        {
                currentCubeColor=0;
                renderer.material.color = GameManager.Instance.cubeColor[currentCubeColor];
                hasBeenSteppedOn = false;
            
            
        }
    }
}
