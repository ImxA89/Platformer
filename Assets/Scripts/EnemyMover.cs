using UnityEngine;

public class EnemyMover
{
    private Transform _transform;

    public EnemyMover(Transform transform)
    {
        _transform = transform;
    }

    public void Move(Vector3 target, int speed)
    {
        _transform.position = Vector3.MoveTowards(_transform.position, target, speed * Time.deltaTime);
    }
}
