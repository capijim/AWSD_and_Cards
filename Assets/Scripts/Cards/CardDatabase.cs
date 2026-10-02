using System.Collections.Generic;
using UnityEngine;

namespace AWSD_and_Cards.Card
{
    /// <summary>
    /// Lớp quản lý cơ sở dữ liệu các lá bài
    /// </summary>
    public class CardDatabase : MonoBehaviour
    {
        public List<Card> cardList = new List<Card>(); // Danh sách tất cả các lá bài trong cơ sở dữ liệu

        void Awake()
        {
            cardList.Add(new Card(1,"Fireball", "Gây sát thương lên kẻ địch", AreaType.ForwardThree, 2, null));
            cardList.Add(new Card(2,"Healing Potion", "Hồi máu cho bản thân", AreaType.SingleForward, 1, null));
            cardList.Add(new Card(3,"Cleave", "Tấn công nhiều kẻ địch phía trước", AreaType.Cleave, 3, null));

        }
        
    }
    
}