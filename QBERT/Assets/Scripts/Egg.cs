using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Player
{
    public override void Update()
    {
        if (!isMoving) //egg script that is derived from the player class. 
        {
            if (Random.Range(0, 2) == 0)
            {
                Jumping(JumpDirection.LeftDown);
            }
            else if (Random.Range(0, 2) == 1)
            {
                Jumping(JumpDirection.RightDown);
            }
        }

        if (transform.position.y <= GameManager.Instance.fallLimit) //as soon as purple egg falls down -7 on the Y, he converts to Coily. 
        {
            if (gameObject.tag == "PurpleEgg")
            {
                Instantiate(GameManager.Instance.CoilyPrefab, GameManager.Instance.RespawnPoint.transform.position, GameManager.Instance.RespawnPoint.transform.rotation);
                Destroy(gameObject);
            }
            if (gameObject.tag == "Slick" || gameObject.tag == "Sam") //if slick or sam touch -7 they get destroyed, or anyone else who is attached to the Egg script. 
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        
    }
}

