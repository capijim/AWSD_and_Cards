using UnityEngine;

namespace AWSD_and_Cards
{
    public static class GridUtility
    {
        // Kích thước mặc định của một ô (Grid Cell Size)
        public const float GridSize = 1f;

        /// <summary>
        /// Làm tròn tọa độ để đảm bảo object nằm chính xác trên lưới.
        /// Giúp Player và Enemy không bao giờ bị lệch khỏi lưới.
        /// </summary>
        public static Vector3 SnapToGrid(Vector3 position)
        {
            float x = Mathf.Round(position.x / GridSize) * GridSize;
            float y = Mathf.Round(position.y / GridSize) * GridSize;
            return new Vector3(x, y, position.z);
        }
    }
}

