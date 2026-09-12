using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AWSD_and_Cards.Enemy;

public enum TurnState { PlayerTurn, EnemyTurn }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TurnState CurrentTurn { get; private set; }

    public List<EnemyController> enemies = new List<EnemyController>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        CurrentTurn = TurnState.PlayerTurn; // Bắt đầu là lượt của người chơi
    }

    // Được gọi bởi Player sau khi di chuyển xong
    public void EndPlayerTurn()
    {
        CurrentTurn = TurnState.EnemyTurn;
        StartCoroutine(EnemyTurnRoutine());
    }

    // Coroutine xử lý lượt của Enemy
    private IEnumerator EnemyTurnRoutine()
    {
        Debug.Log($"[GameManager] EnemyTurnRoutine started. Enemy count: {enemies.Count}");
        yield return new WaitForSeconds(0.2f); // Nghỉ một chút để tạo cảm giác nhịp độ

        // Duyệt qua tất cả quái vật và cho chúng di chuyển
        foreach (EnemyController enemy in enemies)
        {
            if (enemy != null)
            {
                Debug.Log($"[GameManager] Calling TakeTurn for {enemy.gameObject.name}");
                enemy.TakeTurn();
            }
            yield return new WaitForSeconds(0.1f); // Chờ từng con quái di chuyển (nếu muốn)
        }

        yield return new WaitForSeconds(0.2f);
        
        Debug.Log($"[GameManager] Ending EnemyTurn. Back to PlayerTurn.");
        // Trả lại lượt cho người chơi
        CurrentTurn = TurnState.PlayerTurn;
    }
}