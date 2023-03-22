using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isMoving = false;
    public float moveDistance = 1f;
    public float jumpTime;
    public Vector3 pos;


    public virtual void Update()
    {
        
    }

    public void Jumping(JumpDirection dir)
    {
        isMoving = true;
        pos = transform.position;
    
        if (dir == JumpDirection.LeftDown)
        {
            pos += new Vector3(moveDistance, 0f, 0f);
        }
        else if(dir == JumpDirection.LeftUp)
        {
            pos += new Vector3(0f, moveDistance*2, -moveDistance);

        }
        else if (dir == JumpDirection.RightDown)
        {
            pos += new Vector3(0f, 0f, moveDistance);

        }
        else
        {
            pos += new Vector3(-moveDistance, moveDistance*2, 0f);

        }
        StartCoroutine(JumpAnimation(pos));
          
    }

    IEnumerator JumpAnimation(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position,targetPos)>0.2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveDistance * 0.2f);
            yield return new WaitForSeconds(0.02f);
        }
        Invoke("ResetIsMoving", 0.5f);
    }

    private void ResetIsMoving()
    {
        isMoving = false;
    }
}
