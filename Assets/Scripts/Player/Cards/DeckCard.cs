using System.Collections.Generic;
using UnityEngine;

namespace AWSD_and_Cards.Card
{
    public class DeckCard : MonoBehaviour
    {
        [Header("Deck States")]
        public List<Cards> allCards = new List<Cards>();     // Tổng hợp bài ban đầu
        public List<Cards> drawPile = new List<Cards>();     // Chồng bài rút
        public List<Cards> discardPile = new List<Cards>();  // Chồng bài bỏ

        private void Start()
        {
            InitializeDeck();
        }

        public void InitializeDeck()
        {
            drawPile.Clear();
            discardPile.Clear();
            
            // Sao chép bài từ allCards sang drawPile
            drawPile.AddRange(allCards);
            ShuffleDeck();
        }

        public void ShuffleDeck()
        {
            for (int i = 0; i < drawPile.Count; i++)
            {
                Cards temp = drawPile[i];
                int randomIndex = Random.Range(i, drawPile.Count);
                drawPile[i] = drawPile[randomIndex];
                drawPile[randomIndex] = temp;
            }
        }

        public Cards DrawCard()
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

            Cards drawnCard = drawPile[0];
            drawPile.RemoveAt(0);
            return drawnCard;
        }

        public void DiscardCard(Cards card)
        {
            discardPile.Add(card);
        }
    }
}

