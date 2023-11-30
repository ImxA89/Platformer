using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSpawner : MonoBehaviour
{
    [SerializeField] private Banana _bananaPrefab;
    [SerializeField] private float _spawnDelayTime;
    [SerializeField] private Transform _spawnPointPerent;

    private Dictionary<Vector3, bool> _spawnPoints;
    private List<Transform> _bananas;
    private Coroutine _spawnRoutine;
    private float _minSpawnDelayTime = 1f;
    private bool _isFreePoint = true;

    private void OnValidate()
    {
        if (_spawnDelayTime < _minSpawnDelayTime)
            _spawnDelayTime = _minSpawnDelayTime;
    }

    private void Start()
    {
        _spawnPoints = new Dictionary<Vector3, bool>(_spawnPointPerent.childCount);
        _bananas = new List<Transform>(_spawnPointPerent.childCount);

        for (int i = 0; i < _spawnPointPerent.childCount; i++)
            _spawnPoints[_spawnPointPerent.GetChild(i).position] = _isFreePoint;

        _spawnRoutine = StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        for (int i = 0; i < _bananas.Count; i++)
            if (_bananas[i] != null)
                _bananas[i].GetComponent<Banana>().Taken -= OnBananaTaken;

        StopCoroutine(_spawnRoutine);
    }

    private void OnBananaTaken(Transform takenBanana)
    {
        _spawnPoints[takenBanana.position] = _isFreePoint;
        _bananas.Remove(takenBanana);
        takenBanana.GetComponent<Banana>().Taken -= OnBananaTaken;
        Destroy(takenBanana.gameObject);
    }

    private IEnumerator Spawn()
    {
        List<Vector3> freePoints = new List<Vector3>();
        WaitForSeconds delay = new WaitForSeconds(_spawnDelayTime);
        bool isFreePoint = false;
        Banana newBanana;
        Vector3 freePoint;

        while (enabled)
        {
            GetFreePoints(freePoints);

            if (freePoints.Count > 0)
            {
                freePoint = GetRandomFreeSpwanPoint(freePoints);
                newBanana = Instantiate(_bananaPrefab, freePoint, Quaternion.identity);
                _bananas.Add(newBanana.transform);
                _spawnPoints[freePoint] = isFreePoint;
                freePoints.Remove(freePoint);
                newBanana.GetComponent<Banana>().Taken += OnBananaTaken;
            }

            yield return delay;
        }
    }

    private void GetFreePoints(List<Vector3> freePoints)
    {
        bool isPointInList = false;

        foreach (KeyValuePair<Vector3, bool> point in _spawnPoints)
            if (point.Value == _isFreePoint && freePoints.Contains(point.Key) == isPointInList)
                freePoints.Add(point.Key);
    }

    private Vector3 GetRandomFreeSpwanPoint(List<Vector3> freePoints)
    {
        return freePoints[Random.Range(0, freePoints.Count)];
    }
}
