using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static event Action OnAllEnemiesDead;
    private int totalEnemies;

    private void Start()
    {
        totalEnemies = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None).Length;
        EnemyAI.enemydead +=EnemyDied;
    }

    public void EnemyDied()
    {
        totalEnemies--;
        if (totalEnemies <= 0)
        {
            OnAllEnemiesDead?.Invoke();
        }
    }
}
