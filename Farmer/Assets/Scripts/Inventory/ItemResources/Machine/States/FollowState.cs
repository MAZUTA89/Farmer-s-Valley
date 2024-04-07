using Scripts.StateMachine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.InventoryCode.ItemResources
{
    public class FollowState : ItemResourceState
    {
        Vector2 _direction;
        Rigidbody2D _rigidBody;
        public FollowState(ItemResourceStateMachine universalStateMachine,
            ItemResource itemResource,
            ItemSourceSO sourceSO)
            : base(universalStateMachine, itemResource, sourceSO)
        {
            _rigidBody = itemResource.RigidBody;
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void FixedPerform()
        {
            Vector2 newPos = Vector2.Lerp(_rigidBody.position,
                _rigidBody.position + _direction,
                Time.fixedDeltaTime * SourceSO.FollowSpeed);
            _rigidBody.MovePosition(newPos);
        }

        public override void Perform()
        {
            _direction = ItemResource.GetPlayerDirectionVector();
            float distance = ItemResource.GetDistanceToPlayer();

            if (distance >= SourceSO.FollowDistance)
            {
                StateMachine.ChangeOnGroundState();
            }
            if(distance <= 0.1f)
            {
                ItemResource.AddInventoryItem();
                ItemResource.Destroy(ItemResource.gameObject);
            }
        }
    }
}
