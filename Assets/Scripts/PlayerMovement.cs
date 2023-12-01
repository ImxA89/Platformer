using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IContolable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;

    public event Action<Vector2> DirectionTaken;

    private void OnValidate()
    {
        if (_speed < 0)
            _speed = -_speed;

        if (_jumpForce < 0)
            _jumpForce = -_jumpForce;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move(_direction);
    }

    public void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce);
    }

    public void TakeDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private void Move(Vector2 direction)
    {
        DirectionTaken?.Invoke(direction);
        transform.position =
            new Vector3(transform.position.x + (direction.x * Time.fixedDeltaTime * _speed), transform.position.y, 0f);
    }
}
