using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;

    private Vector3 targetPos;
    private GridPosition gridPosition;

    private void Awake()
    {
        targetPos = transform.position;
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetUnitAtGridPosition(gridPosition, this);
    }

    private void Update()
    {

        float stoppingDistance = .1f;
        if(Vector3.Distance(transform.position, targetPos) > stoppingDistance)
        {
            Vector3 moveDir = (targetPos - transform.position).normalized;
            float moveSpeed = 5f;
            transform.position += moveDir * moveSpeed * Time.deltaTime;

            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

            unitAnimator.SetBool("IsWalking", true);
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
        }

        GridPosition newgridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newgridPosition != gridPosition)
        {

        }
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPos = targetPosition;
    }
}
