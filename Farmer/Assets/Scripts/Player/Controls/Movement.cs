using TMPro;
using UnityEngine;
using Zenject;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    InputService _inputService;
    Rigidbody2D _rb;
    Animator _animator;
    Vector2 _delta;

    int _aniX;
    int _aniY;

    [Inject]
    public void Construct(InputService inputService)
    {
        _inputService = inputService;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _aniX = Animator.StringToHash("X");
        _aniY = Animator.StringToHash("Y");
    }

    void Update()
    {
        _delta = _inputService.GetMovement();
        Animate(_delta);
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _delta * Time.fixedDeltaTime);
    }

    void Animate(Vector2 delta)
    {
        _animator.SetFloat(_aniX, delta.x);
        _animator.SetFloat(_aniY, delta.y);
    }
}
