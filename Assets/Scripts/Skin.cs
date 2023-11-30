using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Skin : MonoBehaviour
{
    private Player _player;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private bool _isRunning;
    private bool _isMovingLeft;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _player.DirectionTaken += OnTakenDirection;
    }

    private void OnDisable()
    {
        _player.DirectionTaken -= OnTakenDirection;
    }

    private void OnTakenDirection(Vector2 direction)
    {
        if (direction.x == 0)
        {
            PlayIdleAnimation();
        }
        else if (direction.x > 0)
        {
            PlayRunAnimation();
            _isMovingLeft = false;
            _spriteRenderer.flipX = _isMovingLeft;
        }
        else
        {
            PlayRunAnimation();
            _isMovingLeft = true;
            _spriteRenderer.flipX = _isMovingLeft;
        }
    }

    private void PlayRunAnimation()
    {
        _isRunning = true;
        _animator.SetBool("isRunning", _isRunning);
    }

    private void PlayIdleAnimation()
    {
        _isRunning = false;
        _animator.SetBool("isRunning", _isRunning);
    }
}