using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AWSD_and_Cards.Card
{
    public class Cards
    {
        public string cardName;// Tên của lá bài
        public string description;// Mô tả của lá bài
        public AreaType cardType;// Loại của lá bài (tấn công, hồi máu, buff, debuff, ...)
        public int cost;// Chi phí sử dụng lá bài
        public Sprite cardImage;// Hình ảnh đại diện của lá bài

        /// <summary>
        /// Lấy danh sách các ô bị ảnh hưởng bởi lá bài dựa trên loại hình  
        /// </summary>
        public List<Vector2Int> GetCardArea()
        {
            return AreaHelper.GetArea(cardType);
        }

        public Cards(string name, string desc, int cost, Sprite image)
        {
            this.cardName = name;
            this.description = desc;
            this.cost = cost;
            this.cardImage = image;
        }
    }
    
}
