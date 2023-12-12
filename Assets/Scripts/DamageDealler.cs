using UnityEngine;

public class DamageDealler
{
    private int _damage;
    private float _attackDistance = 1.5f;
    private int _layerMask;

    public DamageDealler(int damage, int targetLayer)
    {
        _damage = damage;
        _layerMask = 1<< targetLayer;
    }

    public void MakeDamage(bool isLookingLeft, Transform transform)
    {
        Vector2 attackDirection;

        if (isLookingLeft)
            attackDirection = -transform.right;
        else
            attackDirection = transform.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, attackDirection, _attackDistance, _layerMask);

            if (hit && hit.transform.TryGetComponent<IAttackable>(out IAttackable attackable))
                attackable.TakeDamage(_damage);
    }
}
