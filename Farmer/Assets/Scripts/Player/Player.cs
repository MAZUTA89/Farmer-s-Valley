using Scripts.FarmGameEvents;
using Scripts.MainMenuCode;
using Scripts.SaveLoader;
using Scripts.SO.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Scripts.PlayerCode
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private float _speed;
        InputService _inputService;
        GameDataState _gameDataState;
        Rigidbody2D _rb;
        PlayerSO _playerSO;
        private Vector2 _currentLookDirection;
        private Vector3 _currentWorldMousePos;

        private int _dirXHash = Animator.StringToHash("DirX");
        private int _dirYHash = Animator.StringToHash("DirY");
        private int _speedHash = Animator.StringToHash("Speed");

        [Inject]
        public void Construct(InputService inputService,
            GameDataState gameDataState,
            PlayerSO playerSO)
        {
            _inputService = inputService;
            _gameDataState = gameDataState;
            _playerSO = playerSO;
        }
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        public void Start()
        {
            _speed = _playerSO.Speed;
        }
        private void Update()
        {
            _currentWorldMousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
        private void FixedUpdate()
        {
            var move = _inputService.GetMovement();
            if (move != Vector2.zero)
            {
                SetLookDirectionFrom(move);
                move = FourDirection(move);
            }
            else
            {
                if (IsMouseOverGameWindow())
                {
                    Vector3 posToMouse = _currentWorldMousePos - transform.position;
                    SetLookDirectionFrom(posToMouse);
                }
            }

            var movement = move * _speed;
            var speed = movement.sqrMagnitude;

            _animator.SetFloat(_dirXHash, _currentLookDirection.x);
            _animator.SetFloat(_dirYHash, _currentLookDirection.y);
            _animator.SetFloat(_speedHash, speed);

            _rb.MovePosition(_rb.position + movement * Time.fixedDeltaTime);
        }
        void SetLookDirectionFrom(Vector2 direction)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                _currentLookDirection = direction.x > 0 ? Vector2.right : Vector2.left;
            }
            else
            {
                _currentLookDirection = direction.y > 0 ? Vector2.up : Vector2.down;
            }
        }
        bool IsMouseOverGameWindow()
        {
            return !(0 > Input.mousePosition.x || 0 > Input.mousePosition.y || Screen.width < Input.mousePosition.x || Screen.height < Input.mousePosition.y);
        }
        Vector2 FourDirection(Vector2 move)
        {
            
            if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
            {
                move = move.x > 0 ? Vector2.right : Vector2.left;
            }
            else
            {
                move = move.y > 0 ? Vector2.up : Vector2.down;
            }
            return move;
        }
        private void OnEnable()
        {
            GameEvents.OnExitTheGameEvent += 
                OnExitTheGame;
        }
        private void OnDisable()
        {
            GameEvents.OnExitTheGameEvent -=
                OnExitTheGame;
        }
        protected void OnExitTheGame()
        {
            PlayerData playerData = new PlayerData();
            playerData.SetPosition(_rb.position);
            _gameDataState.PlayerData = playerData;
        }
        public void Load(PlayerData playerData)
        {
            _rb.position = playerData.GetPosition();
        }
    }
}
