using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> _waveConfigs;
    [SerializeField] private float _timeBetweenWaves = 0f;
    [SerializeField] private bool _isLooping;

    private WaveConfigSO _currentWave;

    private void Start()
    {
        StartCoroutine(SpawnEnemiesWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return _currentWave;
    }

    private IEnumerator SpawnEnemiesWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in _waveConfigs)
            {
                _currentWave = wave;

                for (int i = 0; i < _currentWave.GetEnmyCount(); i++)
                {
                    Instantiate(_currentWave.GetEnemyPrefab(0),
                        _currentWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0,0,180), transform);

                    yield return new WaitForSeconds(_currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(_timeBetweenWaves);
            }
        } while (_isLooping);
    }
}