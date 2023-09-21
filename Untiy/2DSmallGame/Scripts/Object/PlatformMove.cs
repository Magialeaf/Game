using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public Vector3 originPosition, targetPosition, terminalPosition;
    public float moveSpeed;

    private void Start()
    {
        targetPosition = terminalPosition;
        originPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position == targetPosition)
        {
            if (targetPosition == originPosition)
            {
                targetPosition = terminalPosition;
            }
            else
            {
                targetPosition = originPosition;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
