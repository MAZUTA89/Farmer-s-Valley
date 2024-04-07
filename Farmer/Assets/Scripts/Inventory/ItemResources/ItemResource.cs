using System;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using Zenject;

namespace Scripts.InventoryCode.ItemResources
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class ItemResource : MonoBehaviour
    {
        ItemSourceSO _itemSourceSO;
        ItemResourceStateMachine _stateMachine;
        Transform _playerTransform;
        Sprite _icon;
        public InventoryStorage InventoryStorage {  get; private set; }
        public InventoryItem InventoryItem {  get; private set; }
        public Rigidbody2D RigidBody { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public PushState PushState { get; private set; }
        public FollowState FollowState { get; private set; }
        public OnGroundState OnGroundState { get; private set; }
        

        [Inject]
        public void Construct(ItemSourceSO itemSourceSO,
            [Inject(Id = "PlayerTransform")] Transform playerTransform,
            InventoryStorage inventoryStorage)
        {
            InventoryStorage = inventoryStorage;
            _itemSourceSO = itemSourceSO;
            _playerTransform = playerTransform;
        }

        private void Start()
        {
            RigidBody = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            RigidBody.gravityScale = _itemSourceSO.GravityScale;
            SpriteRenderer.sprite = _icon;
            InitializeStateMachine();
        }
        private void Update()
        {
            _stateMachine.Perform();
        }
        private void FixedUpdate()
        {
            _stateMachine.FixedPerform();
        }
        public void Initialize(InventoryItem inventoryItem)
        {
            InventoryItem = inventoryItem;
            _icon = inventoryItem.Icon;
        }
        void InitializeStateMachine()
        {
            _stateMachine = new ItemResourceStateMachine(this);

            PushState = new PushState(_stateMachine, this, _itemSourceSO);
            FollowState = new FollowState(_stateMachine, this, _itemSourceSO);
            OnGroundState = new OnGroundState(_stateMachine, this, _itemSourceSO);
            _stateMachine.Initialize(PushState);
        }
        public void AddInventoryItem()
        {
            InventoryStorage.AddItem(InventoryItem);
        }
        public float GetDistanceToPlayer()
        {
            return Vector2.Distance(_playerTransform.position,
                RigidBody.position);
        }
        public Vector2 GetPlayerDirectionVector()
        {
            Vector2 playerPos = _playerTransform.position;
            Vector2 itemPos = transform.position;
            Vector2 direction = playerPos - itemPos;
            return direction.normalized;
        }
    }
}
