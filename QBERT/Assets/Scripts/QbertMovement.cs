using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QbertMovement : Player
{
    
    private void Start()
    {
        pos = transform.position;
    }

  

    private void OnMove(InputValue value)
    {
        if (!isMoving)
        {
            Vector2 inputVector = value.Get<Vector2>();
            if (inputVector == Vector2.up)
            {
                Jumping(JumpDirection.RightUp);
            }
            else if (inputVector == Vector2.down)
            {
                Jumping(JumpDirection.LeftDown);
            }
            else if (inputVector == Vector2.left)
            {
                Jumping(JumpDirection.LeftUp);
            }
            else if (inputVector == Vector2.right)
            {
                Jumping(JumpDirection.RightDown);
            }
        }
    }

    
}

