using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongWayPlusUgg : Player
{
    private void OnCollisionEnter(Collision collision)//respawns qbert when qbert touches WrongWay & Ugg
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.Respawn();
        }
    }
    public override void Update()//WrongWay & Ugg derive from Player script. 
    {
        if (!isMoving)
        {
            if (Random.Range(0, 2) == 0)
            {
                Jumping(JumpDirection.RightUp);
            }
            else if (Random.Range(0, 2) == 1)
            {
                Jumping(JumpDirection.LeftUp);
            }
        }
        else if(transform.position.y <= GameManager.Instance.fallLimit)
        {
                Destroy(gameObject);
        }
    }
}
