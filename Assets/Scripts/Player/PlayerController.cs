using UnityEngine;

namespace AWSD_and_Cards.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerHealth))]
    [RequireComponent(typeof(PlayerAttack))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Interaction Settings")]
        public LayerMask obstacleLayer; // Layer chứa tường, quái vật...

        private PlayerMovement playerMovement;
        private PlayerHealth playerHealth;
        private PlayerAttack playerAttack;

        private void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
            playerHealth = GetComponent<PlayerHealth>();
            playerAttack = GetComponent<PlayerAttack>();
        }

        private void Start()
        {
            playerMovement.Init();
        }

        private void Update()
        {
            // Cập nhật vị trí di chuyển mượt mà liên tục
            playerMovement.UpdateMovement();

            // Chỉ nhận lệnh nếu đang là lượt của người chơi và nhân vật không đang di chuyển
            if (GameManager.Instance == null || GameManager.Instance.CurrentTurn != TurnState.PlayerTurn) return;
            if (playerMovement.IsMoving) return;

            Vector3 moveDirection = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                moveDirection = Vector3.up;
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                moveDirection = Vector3.down;
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                moveDirection = Vector3.left;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                moveDirection = Vector3.right;

            if (moveDirection != Vector3.zero)
            {
                Vector3 nextPos = transform.position + moveDirection * playerMovement.gridSize;
                TryAction(nextPos);
            }
        }

        private void TryAction(Vector3 targetPos)
        {
            // Kiểm tra xem tại ô đích có gì không
            Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.2f, obstacleLayer);
            
            if (hit != null)
            {
                // Nếu là quái vật -> Tấn công (sử dụng GetComponentInParent đề phòng Collider nằm ở object con)
                Enemy.EnemyHealth enemyHealth = hit.GetComponentInParent<Enemy.EnemyHealth>();
                if (enemyHealth != null)
                {
                    if (playerAttack != null)
                    {
                        playerAttack.Attack(enemyHealth);
                    }
                    else
                    {
                        Debug.LogError("Chưa gắn script PlayerAttack vào Player!");
                    }
                    EndTurn(); // Tấn công xong là mất lượt
                }
                else
                {
                    Debug.Log("Player đụng phải vật cản nhưng vật cản không có EnemyHealth!");
                }
                // Nếu là tường hoặc chướng ngại vật -> Không làm gì cả (không mất lượt)
            }
            else 
            {
                // Đường trống -> Di chuyển
                playerMovement.MoveTo(targetPos);
                EndTurn(); // Di chuyển xong là mất lượt
            }
        }

        private void EndTurn()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.EndPlayerTurn();
            }
        }
    }
}