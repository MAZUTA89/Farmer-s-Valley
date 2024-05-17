using HappyHarvest;
using Scripts.FarmGameEvents;
using Scripts.InventoryCode;
using Scripts.MainMenuCode;
using Scripts.SaveLoader;
using Scripts.SO.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        public Vector3 CurrentWorldMousePos { get; private set; }

        public Transform ItemAttachBone;
        private Dictionary<InventoryItem, ItemInstance> m_ItemVisualInstance { get; set; } = new();
        InventoryItemDataBase _itemDatabase;
        PlayerMoney _playerMoney { get; set; }

        private int _dirXHash = Animator.StringToHash("DirX");
        private int _dirYHash = Animator.StringToHash("DirY");
        private int _speedHash = Animator.StringToHash("Speed");
        private int _useHash = Animator.StringToHash("Use");

        [Inject]
        public void Construct(InputService inputService,
            GameDataState gameDataState,
            InventoryItemDataBase inventoryItemDataBase,
            PlayerMoney playerMoney,
            PlayerSO playerSO)
        {
            _inputService = inputService;
            _gameDataState = gameDataState;
            _playerSO = playerSO;
            _playerMoney = playerMoney;
            _itemDatabase = inventoryItemDataBase;
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
            CurrentWorldMousePos = GetMousePosition();
        }
        private void FixedUpdate()
        {
            Movement();
        }
        void Movement()
        {
            var move = _inputService.GetMovement();
            if (move != Vector2.zero)
            {
                move = FourDirection(move);
                SetLookDirectionFrom(move);
            }
            else
            {
                if (IsMouseOverGameWindow())
                {
                    Vector3 posToMouse = CurrentWorldMousePos - transform.position;
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
        public Vector3 GetMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.forward, Vector3.zero);
            float distance;

            if (plane.Raycast(ray, out distance))
            {
                return ray.GetPoint(distance);
            }
            else { return Vector3.zero; }
        }

        public void UseItemVisual(InventoryItem inventoryItem)
        {
            var previousEquipped = inventoryItem;

            if (m_ItemVisualInstance.ContainsKey(previousEquipped))
            {
                var visual = m_ItemVisualInstance[previousEquipped];

                _animator.SetTrigger(visual.AnimatorHash);

                if (visual.Animator != null)
                {
                    if (!visual.Instance.activeInHierarchy)
                    {
                        //enable all parent as if it's disabled, value cannot be set
                        var current = visual.Instance.transform;
                        while (current != null)
                        {
                            current.gameObject.SetActive(true);
                            current = current.parent;
                        }
                    }

                    visual.Animator.SetFloat(_dirXHash, _currentLookDirection.x);
                    visual.Animator.SetFloat(_dirYHash, _currentLookDirection.y);
                    visual.Animator.SetTrigger(_useHash);

                    //this mean we finished using an item, the entry is now empty, so we need to disable the visual if any
                    if (previousEquipped != null)
                    {
                        //This is a bit of a quick fix, this will let any animation to finish playing before we disable the visual.
                        StartCoroutine(DelayedObjectDisable(previousEquipped));
                    }
                }
            }
            else
            {
                CreateItemVisual(previousEquipped);
                UseItemVisual(inventoryItem);
            }
            
        }
        void CreateItemVisual(InventoryItem item)
        {
            if (item.VisualPrefab != null && !m_ItemVisualInstance.ContainsKey(item))
            {
                var newVisual = Instantiate(item.VisualPrefab, ItemAttachBone, false);
                newVisual.SetActive(false);

                m_ItemVisualInstance[item] = new ItemInstance()
                {
                    Instance = newVisual,
                    Animator = newVisual.GetComponentInChildren<Animator>(),
                    AnimatorHash = Animator.StringToHash(item.PlayerAnimatorTriggerUse)
                };
            }
        }
        IEnumerator DelayedObjectDisable(InventoryItem item)
        {
            yield return new WaitForSeconds(1.0f);
            ToggleVisualExplicit(false, item);
        }
        void ToggleVisualExplicit(bool enable, InventoryItem item)
        {
            if (item != null && m_ItemVisualInstance.TryGetValue(item, out var itemVisual))
            {
                itemVisual.Instance.SetActive(enable);
            }
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
            playerData.Money = _playerMoney.Money;
            _gameDataState.PlayerData = playerData;
        }
        public void Load(PlayerData playerData)
        {
            _rb.position = playerData.GetPosition();
            _playerMoney.Money = playerData.Money;
        }
    }
}
