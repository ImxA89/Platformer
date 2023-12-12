using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField] private int _healPower;

    public int HealPower => _healPower;

    private void OnValidate()
    {
        if (_healPower < 0)
            _healPower = 0;
    }
}
