using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [SerializeField] private Transform _pathPrefab;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _timeBetweenEnemySpawns = 1f;
    [SerializeField] private float _spawnTimerVariance = 0f;
    [SerializeField] private float _minSpawnTime = 0.2f;

    public Transform GetStartingWaypoint()
    {
        return _pathPrefab.GetChild(0);
    }

    public int GetEnmyCount()
    {
        return _enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return _enemyPrefabs[index];
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();

        foreach (Transform child in _pathPrefab)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }

    public float GetMoveSpeed()
    {
        return _moveSpeed;
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(_timeBetweenEnemySpawns - _spawnTimerVariance, _timeBetweenEnemySpawns + _spawnTimerVariance);

        return Mathf.Clamp(spawnTime, _minSpawnTime, float.MaxValue);
    }
}