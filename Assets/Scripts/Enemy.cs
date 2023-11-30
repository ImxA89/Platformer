using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{

    [SerializeField] private Transform[] _points;
    [SerializeField] private uint _speed;

    private SpriteRenderer _spriteRenderer;
    private int _currentTargetIndex = 0;
    private float _reachingDistance = 0.3f;
    private bool _isMovingLeft;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();

        if (FindOutTargetReached())
            ChangeTarget();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _points[_currentTargetIndex].position, _speed * Time.deltaTime);

        if (transform.position.x < _points[_currentTargetIndex].position.x)
        {
            _isMovingLeft = false;
            _spriteRenderer.flipX = _isMovingLeft;
        }
        else
        {
            _isMovingLeft = true;
            _spriteRenderer.flipX = _isMovingLeft;
        }
    }

    private bool FindOutTargetReached()
    {
        bool isReached = false;

        if (Vector2.Distance(_points[_currentTargetIndex].position, transform.position)<_reachingDistance)
            isReached = true;

        return isReached;
    }

    private void ChangeTarget()
    {
        _currentTargetIndex++;
        _currentTargetIndex %= _points.Length;
    }
}