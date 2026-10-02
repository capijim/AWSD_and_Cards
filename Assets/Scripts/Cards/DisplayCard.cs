using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace AWSD_and_Cards.Card
{
    public class DisplayCard : MonoBehaviour
    {

        public List<Card> displayCard = new List<Card>(); // Danh sách các lá bài cần hiển thị

        public CardDatabase cardDatabase; // Tham chiếu đến cơ sở dữ liệu bài
        public int displayId;

        [Header("Card Properties------------------------------------------------------------------")]
        public int cardId;
        public string cardName;
        public string cardDescription;
        public AreaType cardType;// Loại của lá bài (tấn công, hồi máu, buff, debuff, ...)
        public int cardCost;// Chi phí sử dụng lá bài
        public Sprite cardImage;// Hình ảnh đại diện của lá bài
        
        [Header("UI Elements")]
        public TMPro.TextMeshProUGUI cardNameText;
        public TMPro.TextMeshProUGUI cardDescriptionText;
        public UnityEngine.UI.Image cardTypeImage; // Dùng Image thay vì SpriteRenderer cho UI Canvas
        public TMPro.TextMeshProUGUI cardCostText;
        public Image cardImageRenderer;

        void Start()
        {
            displayCard[0] = cardDatabase.cardList[displayId]; 
        }

        // Update is called once per frame
        void Update()
        {
            cardId = displayCard[0].cardId;
            cardName = displayCard[0].cardName;
            cardDescription = displayCard[0].description;
            cardType = displayCard[0].cardType;
            cardCost = displayCard[0].cardCost;
            cardImage = displayCard[0].cardImage;

            cardNameText.text = cardName;
            cardDescriptionText.text = cardDescription;
            cardCostText.text = cardCost.ToString();
            cardImageRenderer.sprite = cardImage;
            cardTypeImage.sprite = null; // Cần thêm logic để hiển thị biểu tượng loại bài dựa trên cardType
        }
    }
}