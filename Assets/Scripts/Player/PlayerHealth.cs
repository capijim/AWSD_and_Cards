using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Events")]
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDie;

    void Start()
    {
        // Khởi tạo đầy máu khi bắt đầu game
        currentHealth = maxHealth;
    }

    // Hàm gọi khi bị quái đánh
    public void TakeDamage(int damageAmount)
    {
        if (currentHealth <= 0) return; // Nếu đã chết thì không nhận sát thương nữa

        currentHealth -= damageAmount;
        Debug.Log($"[PlayerHealth] Bị trừ {damageAmount} máu. Máu hiện tại: {currentHealth}/{maxHealth}");

        // Kích hoạt sự kiện bị đánh (để chạy animation nháy đỏ, cập nhật UI...)
        OnTakeDamage?.Invoke();

        // Kiểm tra xem máu đã hết chưa
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Hàm gọi khi ăn vật phẩm hồi máu
    public void Heal(int healAmount)
    {
        if (currentHealth <= 0) return; // Chết rồi thì không bơm máu được (trừ khi có hồi sinh)

        currentHealth += healAmount;
        
        // Không cho phép máu vượt quá giới hạn tối đa
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log($"[PlayerHealth] Bơm thêm {healAmount} máu. Máu hiện tại: {currentHealth}/{maxHealth}");
    }

    // Hàm gọi khi hết máu
    private void Die()
    {
        Debug.Log("[PlayerHealth] Player đã chết!");
        
        // Kích hoạt sự kiện chết (để chạy animation gục ngã, hiện bảng Game Over...)
        OnDie?.Invoke();
        
        // TODO: Thông báo cho GameManager rằng game đã kết thúc
        // gameObject.SetActive(false);
    }
}

