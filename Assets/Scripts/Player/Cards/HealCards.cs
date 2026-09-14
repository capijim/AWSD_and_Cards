using UnityEngine;

namespace AWSD_and_Cards.Card
{
    public class HealCards : Cards
    {
        public int healAmount; // Số lượng máu hồi phục

        public HealCards(string name, string desc, int cost, Sprite image, int healAmount) : base(name, desc, cost, image)
        {
            this.healAmount = healAmount;
        }
    }
}