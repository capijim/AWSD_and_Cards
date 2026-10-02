using UnityEngine;

namespace AWSD_and_Cards.Card
{
    public class AttackCards : Card
    {
        public int damageAmount; // Số lượng sát thương

        public AttackCards(string name, string desc, AreaType cardType, int cost, Sprite image, int damageAmount) : base(name, desc, cardType, cost, image)
        {
            this.damageAmount = damageAmount;
        }
    }
}