using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QbertMovement : MonoBehaviour
{
    public float moveDistance = 1f;

    private Vector3 pos;
    private bool isMoving = false;

    private void Start()
    {
        pos = transform.position;
    }

    private void Update()
    {
        GameManager.Instance.FallDetection();
    }

    private void OnMove(InputValue value)
    {
        if (!isMoving)
        {
            Vector2 inputVector = value.Get<Vector2>();
            if (inputVector == Vector2.up)
            {
                pos += new Vector3(-moveDistance, 0f, 0f);
            }
            else if (inputVector == Vector2.down)
            {
                pos += new Vector3(moveDistance, 0f, 0f);
            }
            else if (inputVector == Vector2.left)
            {
                pos += new Vector3(0f, 0f, -moveDistance);
            }
            else if (inputVector == Vector2.right)
            {
                pos += new Vector3(0f, 0f, moveDistance);
            }
            isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, moveDistance);
            if (transform.position == pos)
            {
                isMoving = false;
            }
        }
    }
}
