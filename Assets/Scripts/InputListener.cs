using UnityEngine;

[RequireComponent(typeof(IContolable))]
public class InputListener : MonoBehaviour
{
    private IContolable _contolable;
    private Vector2 _moveDirection;

    private void Awake()
    {
        _contolable = GetComponent<IContolable>();
    }

    private void Update()
    {
        ListenMove();
        ListenJump();
        ListenAttack();
    }

    private void ListenMove()
    {
        if (Input.GetKey(KeyCode.A))
            _moveDirection = new Vector2(-1f, 0f);
        else if (Input.GetKey(KeyCode.D))
            _moveDirection = new Vector2(1f, 0f);
        else
            _moveDirection = Vector2.zero;

        _contolable.TakeDirection(_moveDirection);
    }

    private void ListenJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _contolable.Jump();
    }

    private void ListenAttack()
    {
        if (Input.GetKeyDown(KeyCode.W))
            _contolable.Attack();
    }
}
