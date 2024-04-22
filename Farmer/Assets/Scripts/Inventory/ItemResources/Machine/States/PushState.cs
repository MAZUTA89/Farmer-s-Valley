using UnityEngine;

namespace Scripts.InventoryCode.ItemResources
{
    public class PushState : ItemResourceState
    {
        float _pushSpeed;
        float _pushDistance;
        Vector2 _direction;
        Rigidbody2D _rigidBody;
        Vector2 _goal;
        public PushState(ItemResourceStateMachine universalStateMachine,
            ItemResource itemResource, ItemSourceSO itemSourceSO)
            : base(universalStateMachine, itemResource, itemSourceSO)
        {
            _pushSpeed = itemSourceSO.PushSpeed;
            _pushDistance = itemSourceSO.PushDistance;
            _direction = Vector2.down;
            _rigidBody = itemResource.RigidBody;
        }

        public override void Enter()
        {
            _goal = _rigidBody.position + _direction * _pushDistance;
        }

        public override void Exit()
        {
        }

        public override void FixedPerform()
        {
            Vector2 newPos = Vector2.Lerp(
                _rigidBody.position,
                _goal,
                Time.fixedDeltaTime * _pushSpeed);
            _rigidBody.MovePosition(newPos);
        }

        public override void Perform()
        {
            if(Vector2.Distance(_rigidBody.position, _goal) < 
                SourceSO.DistanceToChangeGroundState)
            {
                Debug.Log("Я дошел до точки перехода !");
                StateMachine.ChangeOnGroundState();
            }
            
        }
    }
}
