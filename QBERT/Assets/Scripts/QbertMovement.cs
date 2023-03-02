using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QbertMovement : MonoBehaviour
{
    public float moveDistance = 1f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void OnMove(InputValue value)
    {
        if (!isMoving)
        {
            Vector2 inputVector = value.Get<Vector2>();
            if (inputVector == Vector2.up)
            {
                targetPosition += new Vector3(0f, 0f, -moveDistance);
            }
            else if (inputVector == Vector2.down)
            {
                targetPosition += new Vector3(0f, 0f, moveDistance);
            }
            else if (inputVector == Vector2.left)
            {
                targetPosition += new Vector3(moveDistance, 0f, 0f);
            }
            else if (inputVector == Vector2.right)
            {
                targetPosition += new Vector3(-moveDistance, 0f, 0f);
            }
            isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveDistance);
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }
}

