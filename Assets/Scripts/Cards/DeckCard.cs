using System.Collections.Generic;
using UnityEngine;

namespace AWSD_and_Cards.Card
{
    public class DeckCard : MonoBehaviour
    {
        [Header("Deck States")]
        public List<Card> allCards = new List<Card>();     // Tổng hợp bài ban đầu
        public List<Card> drawPile = new List<Card>();     // Chồng bài rút
        public List<Card> discardPile = new List<Card>();  // Chồng bài bỏ

        private void Start()
        {
            InitializeDeck();
        }

        /// <summary>
        /// Khởi tạo bộ bài: sao chép từ allCards sang drawPile và xào trộn
        /// </summary>
        public void InitializeDeck()
        {
            drawPile.Clear();
            discardPile.Clear();
            
            // Sao chép bài từ allCards sang drawPile
            drawPile.AddRange(allCards);
            ShuffleDeck();
        }

        /// <summary>
        /// Xào trộn bộ bài
        /// </summary>
        public void ShuffleDeck()
        {
            for (int i = 0; i < drawPile.Count; i++)
            {
                Card temp = drawPile[i];
                int randomIndex = Random.Range(i, drawPile.Count);
                drawPile[i] = drawPile[randomIndex];
                drawPile[randomIndex] = temp;
            }
        }

        /// <summary>
        /// Rút một lá bài từ chồng bài rút. Nếu chồng bài rút trống, xào lại từ chồng bài bỏ. Nếu cả hai đều trống, trả về null.
        /// </summary>
        public Card DrawCard()
        {
            if (drawPile.Count == 0)
            {
                if (discardPile.Count == 0)
                {
                    Debug.Log("Không còn bài để rút!");
                    return null; // Cả bộ bài và bài bỏ đều trống
                }

                // Xào lại bài từ chồng bài bỏ
                drawPile.AddRange(discardPile);
                discardPile.Clear();
                ShuffleDeck();
                Debug.Log("Đã xào lại bài bỏ vào bộ bài.");
            }

            Card drawnCard = drawPile[0];
            drawPile.RemoveAt(0);
            return drawnCard;
        }

        /// <summary>
        /// Bỏ một lá bài vào chồng bài bỏ
        /// </summary>
        public void DiscardCard(Card card)
        {
            discardPile.Add(card);
        }
    }
}

