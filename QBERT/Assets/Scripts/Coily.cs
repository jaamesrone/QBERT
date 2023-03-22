using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coily : Player
{ 
    public override void Update()
    {
        if (isMoving == false)
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.Respawn();
        }
    }
}
