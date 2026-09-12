using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AWSD_and_Cards.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [Header("Health Settings")]
        public int maxHealth = 50; // Quái thường có máu ít hơn người chơi
        public int currentHealth;

        [Header("Events")]
        public UnityEvent OnTakeDamage;
        public UnityEvent OnDie;

        void Start()
        {
            currentHealth = maxHealth;
        }

        // Hàm gọi khi quái bị người chơi đánh trúng
        public void TakeDamage(int damageAmount)
        {
            if (currentHealth <= 0) return;

            currentHealth -= damageAmount;
            Debug.Log($"[EnemyHealth] {gameObject.name} bị chém {damageAmount} máu. Còn: {currentHealth}/{maxHealth}");

            // Kích hoạt sự kiện (đổi màu, nháy đỏ, đẩy lùi...)
            OnTakeDamage?.Invoke();

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        // Hàm xử lý khi quái chết
        private void Die()
        {
            Debug.Log($"[EnemyHealth] {gameObject.name} đã bị tiêu diệt!");
            
            OnDie?.Invoke();
            
            // Xóa enemy này khỏi danh sách của GameManager để không bị lỗi gọi lượt khi đã chết
            if (GameManager.Instance != null)
            {
                EnemyController controller = GetComponent<EnemyController>();
                if (controller != null && GameManager.Instance.enemies.Contains(controller))
                {
                    GameManager.Instance.enemies.Remove(controller);
                }
            }

            // Hủy object quái vật khỏi màn hình sau 0.1 giây (để kịp chạy nốt những thứ cuối cùng)
            // Bạn có thể sửa thời gian này thành 1 giây nếu muốn quái từ từ biến mất
            Destroy(gameObject, 0.1f); 
        }
    }
}

