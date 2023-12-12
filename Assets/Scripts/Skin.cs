using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Skin : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private bool _isRunning;
    private bool _isLookingLeft = false;

    public bool IsLookingLeft => _isLookingLeft;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Turn(bool isMovingLeft)
    {
        _isLookingLeft=isMovingLeft;
        _spriteRenderer.flipX = isMovingLeft;
    }

    public void PlayDamageTakenAnimation()
    {
        _animator.SetTrigger(SkinAminator.TakeDamage);
    }

    public void PlayRunAnimation()
    {
        _isRunning = true;
        _animator.SetBool(SkinAminator.IsRunning, _isRunning);
    }

    public void PlayIdleAnimation()
    {
        _isRunning = false;
        _animator.SetBool(SkinAminator.IsRunning, _isRunning);
    }

    public void PlayAttackeAnimation()
    {
        _animator.SetTrigger(SkinAminator.Attack);
    }
}
