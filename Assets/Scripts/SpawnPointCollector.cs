using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointCollector : MonoBehaviour
{
    [SerializeField] private Transform _spawnPointPerent;

    private List<Transform> _spawnPoints;

    public event Action<Transform> PointFreed;

    public void TakeNewBanana(Banana newBanana)
    {
        foreach (Transform point in _spawnPoints)
            if (point.position == newBanana.GetComponent<Transform>().position)
                point.GetComponent<BananaSpawnPoint>().TakeBanana(newBanana);
    }

    private void Start()
    {
        _spawnPoints = new List<Transform>(_spawnPointPerent.childCount);

        for (int i = 0; i < _spawnPointPerent.childCount; i++)
        {
            _spawnPointPerent.GetChild(i).GetComponent<BananaSpawnPoint>().PlayerEntered += BananaTaken;
            _spawnPoints.Add(_spawnPointPerent.GetChild(i));
        }

        SendAllPoints();
    }

    private void OnDisable()
    {
        foreach (Transform spawnPoint in _spawnPoints)
            spawnPoint.GetComponent<BananaSpawnPoint>().PlayerEntered -= BananaTaken;
    }

    private void BananaTaken(Transform spawnPoint)
    {
        PointFreed?.Invoke(spawnPoint);
    }

    private void SendAllPoints()
    {
        foreach (Transform spawnPoint in _spawnPoints)
            PointFreed?.Invoke(spawnPoint);
    }
}
