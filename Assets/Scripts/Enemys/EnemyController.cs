using UnityEngine;

namespace AWSD_and_Cards.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(EnemyAttack))]
    public class EnemyController : MonoBehaviour
    {
        [Header("Interaction Settings")]
        public LayerMask obstacleLayer;
        public float gridSize = 1f;

        private EnemyMovement enemyMovement;
        private EnemyHealth enemyHealth;
        private EnemyAttack enemyAttack;
        private Transform playerTransform;

        private void Awake()
        {
            enemyMovement = GetComponent<EnemyMovement>();
            enemyHealth = GetComponent<EnemyHealth>();
            enemyAttack = GetComponent<EnemyAttack>();
        }

        private void Start()
        {
            enemyMovement.Init();
            
            // Đăng ký Enemy này vào danh sách quản lý của GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.enemies.Add(this);
            }

            FindPlayer();
        }

        private void Update()
        {
            // Cập nhật vị trí di chuyển mượt mà liên tục
            enemyMovement.UpdateMovement();
        }

        public void TakeTurn()
        {
            if (enemyMovement.IsMoving) return;

            // Nếu chưa tìm thấy Player, thử tìm lại
            if (playerTransform == null)
            {
                FindPlayer();
            }
            
            if (playerTransform == null) return;

            // Tính khoảng cách từ enemy đến player
            float diffX = playerTransform.position.x - transform.position.x;
            float diffY = playerTransform.position.y - transform.position.y;

            // Kiểm tra xem Enemy đã đứng sát Player chưa (khoảng cách 1 ô)
            float distanceToPlayer = Mathf.Abs(diffX) + Mathf.Abs(diffY);
            
            if (distanceToPlayer <= gridSize + 0.1f && distanceToPlayer > 0.1f)
            {
                PlayerHealth pHealth = playerTransform.GetComponent<PlayerHealth>();
                if (pHealth != null)
                {
                    enemyAttack.Attack(pHealth);
                }
                return;
            }

            // Tìm hướng đi tốt nhất
            Vector3 moveDirection = GetBestMoveDirection(diffX, diffY);

            if (moveDirection != Vector3.zero)
            {
                Vector3 targetPosition = transform.position + moveDirection * gridSize;
                enemyMovement.MoveTo(targetPosition);
            }
        }

        private void FindPlayer()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerTransform = player.transform;
            else
            {
                PlayerMovement pm = FindObjectOfType<PlayerMovement>();
                if (pm != null) playerTransform = pm.transform;
            }
        }

        private Vector3 GetBestMoveDirection(float diffX, float diffY)
        {
            Vector3 dirX = Mathf.Abs(diffX) > 0.1f ? new Vector3(Mathf.Sign(diffX), 0f, 0f) : Vector3.zero;
            Vector3 dirY = Mathf.Abs(diffY) > 0.1f ? new Vector3(0f, Mathf.Sign(diffY), 0f) : Vector3.zero;

            if (Mathf.Abs(diffX) > Mathf.Abs(diffY))
            {
                if (dirX != Vector3.zero && CanMoveTo(transform.position + dirX * gridSize)) return dirX;
                if (dirY != Vector3.zero && CanMoveTo(transform.position + dirY * gridSize)) return dirY;
            }
            else
            {
                if (dirY != Vector3.zero && CanMoveTo(transform.position + dirY * gridSize)) return dirY;
                if (dirX != Vector3.zero && CanMoveTo(transform.position + dirX * gridSize)) return dirX;
            }

            return Vector3.zero;
        }

        private bool CanMoveTo(Vector3 targetPos)
        {
            // 1. Kiểm tra xem ô đích có phải là ô Player đang đứng không (để tránh đi đè lên Player)
            if (playerTransform != null && Vector3.Distance(targetPos, playerTransform.position) < 0.1f)
            {
                return false;
            }

            // 2. Kiểm tra xem tại ô đích có vật cản nào không (Enemy khác, Tường...)
            Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.2f, obstacleLayer);
            
            return hit == null;
        }
    }
}