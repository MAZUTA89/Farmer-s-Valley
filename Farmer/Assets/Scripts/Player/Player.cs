using Scripts.FarmGameEvents;
using Scripts.MainMenuCode;
using Scripts.SaveLoader;
using Scripts.SO.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.PlayerCode
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        InputService _inputService;
        GameDataState _gameDataState;
        Rigidbody2D _rb;
        PlayerSO _playerSO;
        Animator _animator;
        Movement _movement;
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
            _animator = GetComponent<Animator>();
            _movement = new Movement(_inputService,
                _rb, _animator, _playerSO);
        }
        private void Update()
        {
            _movement.Update();
        }
        private void FixedUpdate()
        {
            _movement.FixedUpdate();
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
