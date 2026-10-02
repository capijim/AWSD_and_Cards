using System.Collections.Generic;
using UnityEngine;

namespace AWSD_and_Cards.Card
{
    public enum AreaType
    {
        SingleForward,  // 1 ô ngay phía trước
        ForwardTwo,     // 2 ô thẳng phía trước (chọc)
        ForwardThree,   // 3 ô thẳng phía trước (giáo dài)
        Cleave,         // 3 ô ngang ngay trước mặt (chém rộng)
        Cone,           // Hình nón: 1 ô trước, 3 ô hàng tiếp theo
        Cross,          // Chữ thập (4 hướng: trên, dưới, trái, phải)
        XShape,         // Chữ X (4 góc chéo quanh nhân vật)
        Surrounding8,   // 8 ô xung quanh (3x3 trừ ô trung tâm - AoE nhỏ)
        Surrounding24,  // 24 ô xung quanh (5x5 trừ ô trung tâm - AoE lớn)
        Custom          // Tự định nghĩa bằng list Vector2Int
    }

    public static class AreaHelper
    {
        /// <summary>
        /// Trả về danh sách tọa độ (Vector2Int) các ô bị ảnh hưởng dựa trên loại hình khu vực.
        /// Tọa độ trả về là tương đối, với (0,0) là vị trí nhân vật, (0,1) là hướng phía trước.
        /// </summary>
        public static List<Vector2Int> GetArea(AreaType type)
        {
            List<Vector2Int> area = new List<Vector2Int>();
            switch (type)
            {
                case AreaType.SingleForward:
                    area.Add(new Vector2Int(0, 1));
                    break;
                
                case AreaType.ForwardTwo:
                    area.Add(new Vector2Int(0, 1));
                    area.Add(new Vector2Int(0, 2));
                    break;
                
                case AreaType.ForwardThree:
                    area.Add(new Vector2Int(0, 1));
                    area.Add(new Vector2Int(0, 2));
                    area.Add(new Vector2Int(0, 3));
                    break;
                
                case AreaType.Cleave:
                    area.Add(new Vector2Int(-1, 1));
                    area.Add(new Vector2Int(0, 1));
                    area.Add(new Vector2Int(1, 1));
                    break;
                
                case AreaType.Cone:
                    // Ô đầu tiên
                    area.Add(new Vector2Int(0, 1));
                    // 3 ô tiếp theo
                    area.Add(new Vector2Int(-1, 2));
                    area.Add(new Vector2Int(0, 2));
                    area.Add(new Vector2Int(1, 2));
                    break;
                
                case AreaType.Cross:
                    area.Add(new Vector2Int(0, 1));
                    area.Add(new Vector2Int(0, -1));
                    area.Add(new Vector2Int(-1, 0));
                    area.Add(new Vector2Int(1, 0));
                    break;
                
                case AreaType.XShape:
                    area.Add(new Vector2Int(-1, 1));
                    area.Add(new Vector2Int(1, 1));
                    area.Add(new Vector2Int(-1, -1));
                    area.Add(new Vector2Int(1, -1));
                    break;
                
                case AreaType.Surrounding8:
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if (x == 0 && y == 0) continue;
                            area.Add(new Vector2Int(x, y));
                        }
                    }
                    break;
                
                case AreaType.Surrounding24:
                    for (int x = -2; x <= 2; x++)
                    {
                        for (int y = -2; y <= 2; y++)
                        {
                            if (x == 0 && y == 0) continue;
                            area.Add(new Vector2Int(x, y));
                        }
                    }
                    break;
            }
            return area;
        }
    }
}
