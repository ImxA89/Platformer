using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnPointCollector))]
public class BananaSpawner : MonoBehaviour
{
    [SerializeField] private Banana _bananaPrefab;
    [SerializeField] private float _spawnDelayTime;

    private List<Transform> _pointsForSpawn;
    private SpawnPointCollector _collector;
    private Coroutine _spawnRoutine;
    private float _minSpawnDelayTime = 1f;
    private bool _isSpawnRoutineWorking = false;

    private void OnValidate()
    {
        if (_spawnDelayTime < _minSpawnDelayTime)
            _spawnDelayTime = _minSpawnDelayTime;
    }

    private void Awake()
    {
        _pointsForSpawn = new List<Transform>();
        _collector = GetComponent<SpawnPointCollector>();
        _collector.PointFreed += OnSpawnPointFreed;
    }

    private void OnDisable()
    {
        _collector.PointFreed -= OnSpawnPointFreed;

        if (_isSpawnRoutineWorking)
            StopCoroutine(_spawnRoutine);
    }

    private void OnSpawnPointFreed(Transform pointForSpawn)
    {
        bool isContain = false;

        if (_pointsForSpawn.Contains(pointForSpawn) == isContain)
            _pointsForSpawn.Add(pointForSpawn);

        if (_isSpawnRoutineWorking == false)
            StartSpawnCoroutine();
    }

    private void StartSpawnCoroutine()
    {
        _isSpawnRoutineWorking = true;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds delay = new WaitForSeconds(_spawnDelayTime);
        Transform freePoint;
        Banana newBanana;

        while (_pointsForSpawn.Count > 0)
        {
            freePoint = GetRandomFreeSpwanPoint(_pointsForSpawn);
            newBanana = Instantiate(_bananaPrefab, freePoint.position, Quaternion.identity);
            _pointsForSpawn.Remove(freePoint);
            _collector.TakeNewBanana(newBanana);

            yield return delay;
        }

        _isSpawnRoutineWorking = false;
    }

    private Transform GetRandomFreeSpwanPoint(List<Transform> freePoints)
    {
        return freePoints[Random.Range(0, freePoints.Count)];
    }
}
