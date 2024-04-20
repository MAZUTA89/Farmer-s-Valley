using Scripts.FarmGameEvents;
using Scripts.SaveLoader;
using Scripts.SO;
using Scripts.SO.Player;
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
    PlayerSO _playerSO;
    Vector2 _loadedPosition;
    int _aniX;
    int _aniY;

    GameDataState _gameDataState;

    [Inject]
    public void Construct(InputService inputService, PlayerSO playerSO,
        GameDataState gameDataState)
    {
        _inputService = inputService;
        _playerSO = playerSO;
        _gameDataState = gameDataState;
    }
    private void OnEnable()
    {
        GameEvents.OnExitTheGameEvent += OnExitTheGame;
    }
    private void OnDisable()
    {
        GameEvents.OnExitTheGameEvent -= OnExitTheGame;
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _aniX = Animator.StringToHash("X");
        _aniY = Animator.StringToHash("Y");
        if(_loadedPosition != Vector2.zero)
        {
            _rb.position = _loadedPosition;
        }
    }

    void Update()
    {
        _delta = _inputService.GetMovement();
        Animate(_delta);
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _delta * Time.fixedDeltaTime * _playerSO.Speed);
    }

    void Animate(Vector2 delta)
    {
        _animator.SetFloat(_aniX, delta.x);
        _animator.SetFloat(_aniY, delta.y);
    }

    public void OnExitTheGame()
    {
        PlayerData playerData = new PlayerData();
        playerData.SetPosition(_rb.position);
        _gameDataState.PlayerData = playerData;
    }
    public void SetLoadedPosition(Vector2 position)
    {
        _loadedPosition = position;
    }
}
