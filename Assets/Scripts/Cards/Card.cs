using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AWSD_and_Cards.Card
{
    public class Card : ScriptableObject
    {
        public int cardId;// ID duy nhất của lá bài
        public string cardName;// Tên của lá bài
        public string description;// Mô tả của lá bài
        public AreaType cardType;// phạm vi tấn công
        public int cardCost;// Chi phí sử dụng lá bài
        public Sprite cardImage;// Hình ảnh đại diện của lá bài

        /// <summary>
        /// Lấy danh sách các ô bị ảnh hưởng bởi lá bài dựa trên loại hình  
        /// </summary>
        public List<Vector2Int> GetCardArea()
        {
            return AreaHelper.GetArea(cardType);
        }
        /// <summary>
        /// Khởi tạo một lá bài mới
        /// </summary>
        public Card(){}

        public Card(string name, string desc, AreaType type, int cost, Sprite image)
        {
            this.cardId = Random.Range(1, 1000); // Tạo ID ngẫu nhiên
            this.cardName = name;
            this.description = desc;
            this.cardType = type;
            this.cardCost = cost;
            this.cardImage = image;
        }
        public Card(int id, string name, string desc, AreaType type, int cost, Sprite image)
        {
            this.cardId = id; // Sử dụng ID được cung cấp
            this.cardName = name;
            this.description = desc;
            this.cardType = type;
            this.cardCost = cost;
            this.cardImage = image;
        }
    }
    
}
