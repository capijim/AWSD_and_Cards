using UnityEngine;

namespace AWSD_and_Cards.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Attack Settings")]
        public int attackDamage = 20;

        // Được gọi từ PlayerController
        public void Attack(Enemy.EnemyHealth target)
        {
            if (target != null)
            {
                target.TakeDamage(attackDamage);
            }
        }
    }
}

