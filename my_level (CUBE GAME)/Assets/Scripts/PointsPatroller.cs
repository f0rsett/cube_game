using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPatroller : MonoBehaviour
{
    [SerializeField] private Transform[] _patrollingPoints;
    [SerializeField] private float _patrolDelay;
    [SerializeField] private float _step;

    [Header("Objects")]
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _enemy;

    private float _timer = 0f;
    private int _currentPatrolPointIndex = 0;


    private void Awake()
    {
        _timer = _patrolDelay;
    }

    private void Update()
    {


        PatrolTimerTick();
        MoveToPoint(_currentPatrolPointIndex);
        CubesCollision();
    }

    private void PatrolTimerTick()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            int nextPointIndex = _currentPatrolPointIndex + 1;

            if (nextPointIndex >= _patrollingPoints.Length)
                nextPointIndex = 0;

            _currentPatrolPointIndex = nextPointIndex;
            _timer = _patrolDelay;
        }
    }
    private void CubesCollision()
    {
        var flatEnemyPosition = new Vector2(_enemy.transform.position.x, _enemy.transform.position.z);
        var flatPlayerPosition = new Vector2(_player.transform.position.x, _player.transform.position.z);

        if (flatEnemyPosition == flatPlayerPosition)
        {
            _player.Kill();

            Destroy(_player.gameObject);
        }
    }
    private void MoveToPoint(int pointIndex)
    {
        transform.position = new Vector3(_patrollingPoints[pointIndex].position.x, transform.position.y, _patrollingPoints[pointIndex].position.z);
    }
}
