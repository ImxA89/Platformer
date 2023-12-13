using UnityEngine;

[RequireComponent(typeof(Skin))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAction : MonoBehaviour, IContolable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Skin _skin;
    private Rigidbody2D _rigidbody2D;
    private Player _player;
    private Vector2 _direction;
    private bool _isLookingLeft = false;

    private void OnValidate()
    {
        if (_speed < 0)
            _speed = -_speed;

        if (_jumpForce < 0)
            _jumpForce = -_jumpForce;
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _skin = GetComponent<Skin>();
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

        if (_direction.x < 0)
        {
            _isLookingLeft = true;
            _skin.PlayRunAnimation();
        }
        else if (_direction.x > 0)
        {
            _isLookingLeft = false;
            _skin.PlayRunAnimation();
        }
        else
        {
            _skin.PlayIdleAnimation();
        }

        _skin.Turn(_isLookingLeft);
    }

    private void Move(Vector2 direction)
    {
        transform.position =
            new Vector3(transform.position.x + (direction.x * Time.fixedDeltaTime * _speed), transform.position.y, 0f);
    }

    public void Attack()
    {
        _skin.PlayAttackeAnimation();
        _player.Attack(_isLookingLeft);
    }
}
