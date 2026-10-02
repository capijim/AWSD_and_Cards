using UnityEngine;

namespace AWSD_and_Cards.Card
{
    public class HealCards : Card
    {
        public int healAmount; // Số lượng máu hồi phục

        public HealCards(string name, string desc, AreaType cardType, int cost, Sprite image, int healAmount) : base(name, desc, cardType, cost, image)
        {
            this.healAmount = healAmount;
        }
    }
}