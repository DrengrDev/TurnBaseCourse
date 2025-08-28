using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPos;

    private void Update()
    {
        float stoppingDistance = .1f;
        if(Vector3.Distance(transform.position, targetPos) > stoppingDistance)
        {
            Vector3 moveDir = (targetPos - transform.position).normalized;
            float moveSpeed = 5f;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Move(new Vector3(4, 0, 4));
        }
    }

    private void Move(Vector3 targetPosition)
    {
        this.targetPos = targetPosition;
    }
}
