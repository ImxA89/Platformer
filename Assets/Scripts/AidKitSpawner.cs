using UnityEngine;

public class AidKitSpawner : MonoBehaviour
{
    [SerializeField] private Transform _aidKitSpawnPointPerent;
    [SerializeField] private AidKit _aidKitPrefab;

    private Transform[] _aidKitSpawnPoints;

    private void Awake()
    {
        _aidKitSpawnPoints = new Transform[_aidKitSpawnPointPerent.childCount];

        for (int i = 0; i < _aidKitSpawnPointPerent.childCount; i++)
            _aidKitSpawnPoints[i] = _aidKitSpawnPointPerent.GetChild(i).transform;
    }

    private void Start()
    {
        for (int i = 0; i < _aidKitSpawnPoints.Length; i++)
            Instantiate(_aidKitPrefab, _aidKitSpawnPoints[i].position, Quaternion.identity);
    }
}
