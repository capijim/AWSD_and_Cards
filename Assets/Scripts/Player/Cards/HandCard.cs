using System.Collections.Generic;
using UnityEngine;

namespace AWSD_and_Cards.Card
{
    public class HandCard : MonoBehaviour
    {
        [Header("Hand Settings")]
        public int maxHandSize = 5;
        
        [Header("Current Hand")]
        public List<Cards> cardsInHand = new List<Cards>();

        public DeckCard deckCard; // Tham chiếu đến bộ bài

        private void Start()
        {
            if (deckCard == null)
            {
                deckCard = FindObjectOfType<DeckCard>();
            }
        }

        // Rút một lá từ DeckCard vào tay
        public void DrawToHand()
        {
            if (cardsInHand.Count >= maxHandSize)
            {
                Debug.Log("Tay đã đầy bài, không thể rút thêm!");
                return;
            }

            if (deckCard != null)
            {
                Cards drawnCard = deckCard.DrawCard();
                if (drawnCard != null)
                {
                    cardsInHand.Add(drawnCard);
                    Debug.Log($"Đã rút bài: {drawnCard.cardName}");
                }
            }
        }

        // Đánh một lá bài từ tay (theo index)
        public void PlayCard(int index)
        {
            if (index < 0 || index >= cardsInHand.Count) return;

            Cards playedCard = cardsInHand[index];
            Debug.Log($"Đã chơi lá bài: {playedCard.cardName}");

            // TODO: Gọi logic thực hiện hiệu ứng của lá bài tại đây (tấn công, hồi máu, ...)
            
            // Xóa khỏi tay và đưa vào chồng bài bỏ
            cardsInHand.RemoveAt(index);
            if (deckCard != null)
            {
                deckCard.DiscardCard(playedCard);
            }
        }

        // Vứt bỏ một lá bài (không kích hoạt hiệu ứng)
        public void DiscardFromHand(int index)
        {
            if (index < 0 || index >= cardsInHand.Count) return;

            Cards discardedCard = cardsInHand[index];
            cardsInHand.RemoveAt(index);
            
            if (deckCard != null)
            {
                deckCard.DiscardCard(discardedCard);
            }
        }

        // Xóa toàn bộ bài trên tay (VD: khi kết thúc lượt)
        public void DiscardAllHand()
        {
            if (deckCard != null)
            {
                foreach (Cards card in cardsInHand)
                {
                    deckCard.DiscardCard(card);
                }
            }
            cardsInHand.Clear();
        }
    }
}

