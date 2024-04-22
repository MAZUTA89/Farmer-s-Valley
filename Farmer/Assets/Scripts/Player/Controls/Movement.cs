using Scripts.SO.Player;
using UnityEngine;


public class Movement
{
    InputService _inputService;
    Rigidbody2D _rb;
    Animator _animator;
    Vector2 _delta;
    PlayerSO _playerSO;
    int _aniX;
    int _aniY;

    public Movement(InputService inputService, Rigidbody2D rb,
        Animator animator,
        PlayerSO playerSO
        )
    {
        _inputService = inputService;
        _rb = rb;
        _animator = animator;
        _playerSO = playerSO;

        _aniX = Animator.StringToHash("X");
        _aniY = Animator.StringToHash("Y");
    }
    public void Update()
    {
        _delta = _inputService.GetMovement();
        Animate(_delta);
    }
    public void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _delta * Time.fixedDeltaTime * _playerSO.Speed);
    }

    void Animate(Vector2 delta)
    {
        _animator.SetFloat(_aniX, delta.x);
        _animator.SetFloat(_aniY, delta.y);
    }
}
