using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] private Player _player;

    private int _healPower = 5;
    private int _damage = 5;

    public void OnHealButtonClicked()
    {
        _player.TakeHeal(_healPower);
    }

    public void OnDamageButtonClicked()
    {
        _player.TakeDamage(_damage);
    }
}
