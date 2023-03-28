using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coily : Player
{
    bool updateScore = false;
    public override void Update()
    {
        if (isMoving == false) //coily jump directino derived from my player script
        {
            Vector3 playerPos = GameManager.Instance.Player.transform.position;
            if (transform.position.x >= playerPos.x)
            {
                if (transform.position.y >= playerPos.y)
                {
                    Jumping(JumpDirection.RightDown);
                }
                else
                {
                    Jumping(JumpDirection.RightUp);
                }
            }
            else
            {
                if (transform.position.y >= playerPos.y)
                {
                    Jumping(JumpDirection.LeftDown);
                }
                else
                {
                    Jumping(JumpDirection.LeftUp);
                }
            }
        }
        else if(updateScore == false) //updates score whenever coily jumps after qbert. 
        {
            updateScore = true;
            if (transform.position.y <= GameManager.Instance.fallLimit)
            {
              Debug.Log("hi i fell ");
              GameManager.Instance.score += 500;
            }
        }
    }
}
