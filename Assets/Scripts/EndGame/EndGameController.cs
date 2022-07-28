using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndGameController : MonoBehaviour
{
    public event UnityAction Won;

    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private GameObject _endScreen;

    private void OnEnable()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Destoryed += OnEnemyDestroyed;
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Destoryed -= OnEnemyDestroyed;
        }
    }

    private void OnEnemyDestroyed(Enemy enemy)
    {
        enemy.Destoryed -= OnEnemyDestroyed;
        _enemies.Remove(enemy);

        CheckWin();
    }

    private void CheckWin()
    {
        if (_enemies.Count == 0)
        {
            Won?.Invoke();
            _endScreen.SetActive(true);
        }
    }
}
