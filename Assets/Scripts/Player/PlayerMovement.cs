using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    public bool IsMoving { get; private set; } = false;
    private Vector3 targetPosition;

    public void Init()
    {
        // Snap vị trí ban đầu vào lưới để tránh bị lệch ngay từ đầu
        transform.position = AWSD_and_Cards.GridUtility.SnapToGrid(transform.position);
        targetPosition = transform.position;
    }

    // Được gọi liên tục từ PlayerController
    public void UpdateMovement()
    {
        if (IsMoving)
        {
            // Di chuyển mượt mà tới ô mục tiêu
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) <= 0.001f)
            {
                transform.position = targetPosition; // Snap cứng vào lưới
                IsMoving = false;
            }
        }
    }

    // Bắt đầu di chuyển tới ô mới
    public void MoveTo(Vector3 nextPos)
    {
        targetPosition = AWSD_and_Cards.GridUtility.SnapToGrid(nextPos);
        IsMoving = true;
    }
}
