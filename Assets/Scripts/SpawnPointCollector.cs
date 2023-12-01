using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointCollector : MonoBehaviour
{
    [SerializeField] private Transform _spawnPointPerent;

    private List<BananaSpawnPoint> _spawnPoints;
    private Transform _tempSpawnPoint;

    public event Action<Transform> PointFreed;

    private void Start()
    {
        _spawnPoints = new List<BananaSpawnPoint>();

        for (int i = 0; i < _spawnPointPerent.childCount; i++)
        {
            _tempSpawnPoint = _spawnPointPerent.GetChild(i);
            _spawnPoints.Add(_tempSpawnPoint.GetComponent<BananaSpawnPoint>());
            _tempSpawnPoint.GetComponent<BananaSpawnPoint>().PlayerEntered += BananaTaken;
        }

        SendAllPoints();
    }

    private void OnDisable()
    {
        foreach (BananaSpawnPoint spawnPoint in _spawnPoints)
            spawnPoint.PlayerEntered -= BananaTaken;
    }

    public void TakeNewBanana(Banana newBanana)
    {
        foreach (BananaSpawnPoint point in _spawnPoints)
            if (point.transform.position == newBanana.transform.position)
                point.TakeBanana(newBanana);
    }

    private void BananaTaken(Transform spawnPoint)
    {
        PointFreed?.Invoke(spawnPoint);
    }

    private void SendAllPoints()
    {
        foreach (BananaSpawnPoint spawnPoint in _spawnPoints)
            PointFreed?.Invoke(spawnPoint.transform);
    }
}
