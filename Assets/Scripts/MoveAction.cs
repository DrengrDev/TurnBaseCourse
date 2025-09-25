using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPos;
    private Unit unit;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        targetPos = transform.position;
    }

    private void Update()
    {
        //controlling animation based on unit movement
        float stoppingDistance = .1f;
        if (Vector3.Distance(transform.position, targetPos) > stoppingDistance)
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
    }

    public void Move(GridPosition gridPosition)
    {
        this.targetPos = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    //List of grid positions for all of the valid actions for this specific action
    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for(int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for(int z = -maxMoveDistance; z <=maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue; //Skips to next iteration if not valid
                }

                if(unitGridPosition == testGridPosition)
                {
                    //Same Grid Position where the unit is already at
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    //Grid Position already accupied with another Unit
                    continue;
                }

                //Will continue here if is valid
                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}
