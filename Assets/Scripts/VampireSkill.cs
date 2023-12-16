using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class VampireSkill : MonoBehaviour
{
    [SerializeField][Range(1, 3)] private int _skillPower;
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField][Min(1f)] float _skillRadius;

    private Coroutine _transferHealthRoutine;
    private Player _player;
    private IAttackable _target;
    private int _skillDuration = 6;
    private int _attackTimesInOneSecond = 2;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void StartSkill()
    {
        if (_transferHealthRoutine != null)
            StopCoroutine(_transferHealthRoutine);

        _transferHealthRoutine = StartCoroutine(TransferHealth());
    }

    private IEnumerator TransferHealth()
    {
        WaitForSeconds delay = new WaitForSeconds((float)1 / _attackTimesInOneSecond);
        int timer = 0;
        _target = null;

        while (enabled && timer < _skillDuration * _attackTimesInOneSecond)
        {
            timer++;
            FindNearestTarget();

            if (_target != null)
            {
                _target.TakeDamage(_skillPower);
                _player.TakeHeal(_skillPower);
            }

            yield return delay;
        }
    }

    private void FindNearestTarget()
    {
        float minDistance = _skillRadius;
        float newTargetDistanse = _skillRadius;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _skillRadius, _enemyLayerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].TryGetComponent<Enemy>(out Enemy enemy))
            {
                newTargetDistanse = Vector3.Distance(transform.position, enemy.transform.position);

                if (newTargetDistanse <= minDistance)
                {
                    minDistance = newTargetDistanse;
                    _target = enemy;
                }
            }
        }
    }
}
