using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game.lib
{
    public class Movement : MonoBehaviour
    {
        public bool isMoving = false;

        [Header("By Path")]
        [SerializeField]
        private bool isMovingByPath = false;
        [SerializeField]
        private bool movingAsLoop = false;
        [SerializeField]
        private GameObject path;
        [SerializeField]
        private float speedMoving = 10;
        [SerializeField]
        private MovementPoint[] movementPoint;

        private int _nextPointMoveIndex = 1;

        // Use this for initialization
        void Start()
        {
            if (isMovingByPath)
                movementPoint = path.GetComponentsInChildren<MovementPoint>();

        }

        // Update is called once per frame
        void FixedUpdate()
        {

            Moving();

        }

        void Moving()
        {
            if (!isMoving)
                return;

            if (isMovingByPath && 0 < movementPoint.Length)
            {
                transform.position = Vector3.MoveTowards(transform.position, movementPoint[_nextPointMoveIndex].transform.position, speedMoving * Time.fixedDeltaTime);

                if (Vector3.Distance(transform.position, movementPoint[_nextPointMoveIndex].transform.position) <= 0.05f)
                    _nextPointMoveIndex++;

                if (movementPoint.Length <= _nextPointMoveIndex)
                {
                    if (movingAsLoop)
                    {
                        _nextPointMoveIndex = 0;
                    }
                    else
                    {
                        isMoving = false;
                        _nextPointMoveIndex = 0;
                    }
                }
                return;
            }

        }
    }
}