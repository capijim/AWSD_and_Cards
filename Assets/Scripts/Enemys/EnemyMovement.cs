using UnityEngine;

namespace AWSD_and_Cards.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 5f;

        public bool IsMoving { get; private set; } = false;
        private Vector3 targetPosition;

        public void Init()
        {
            // Snap vị trí ban đầu vào lưới
            transform.position = GridUtility.SnapToGrid(transform.position);
            targetPosition = transform.position;
        }

        // Được gọi liên tục từ EnemyController
        public void UpdateMovement()
        {
            if (IsMoving)
            {
                // Di chuyển mượt mà tới ô mục tiêu
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, targetPosition) <= 0.001f)
                {
                    transform.position = targetPosition; // Snap vào lưới
                    IsMoving = false;
                }
            }
        }

        public void MoveTo(Vector3 nextPos)
        {
            targetPosition = GridUtility.SnapToGrid(nextPos);
            IsMoving = true;
        }
    }
}
