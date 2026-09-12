using UnityEngine;

namespace AWSD_and_Cards.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [Header("Attack Settings")]
        public int attackDamage = 10;

        // Được gọi từ EnemyController
        public void Attack(PlayerHealth target)
        {
            if (target != null)
            {
                target.TakeDamage(attackDamage);
            }
        }
    }
}

