using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    private WaveConfigSO _waveConfig;
    List<Transform> _wayPoints;

    private int _waypointIndex = 0;

    private void Awake()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Start()
    {
        _waveConfig = _enemySpawner.GetCurrentWave();
        _wayPoints = _waveConfig.GetWaypoints();
        transform.position = _wayPoints[_waypointIndex].position;
    }

    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if(_waypointIndex < _wayPoints.Count)
        {
            Vector3 targetPosition = _wayPoints[_waypointIndex].position;

            float delta = _waveConfig.GetMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);

            if(transform.position == targetPosition)
            {
                _waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}