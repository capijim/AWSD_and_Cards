using UnityEngine;

namespace AWSD_and_Cards.Card
{
    public class AttackCards : Cards
    {
        public int damageAmount; // Số lượng sát thương

        public AttackCards(string name, string desc, int cost, Sprite image, int damageAmount) : base(name, desc, cost, image)
        {
            this.damageAmount = damageAmount;
        }
    }
}