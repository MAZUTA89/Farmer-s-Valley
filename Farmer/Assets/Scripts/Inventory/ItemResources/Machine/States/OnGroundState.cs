using UnityEngine;

namespace Scripts.InventoryCode.ItemResources
{
    public class OnGroundState : ItemResourceState
    {
        public OnGroundState(ItemResourceStateMachine universalStateMachine,
            ItemResource itemResource,
            ItemSourceSO sourceSO)
            : base(universalStateMachine, itemResource, sourceSO)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void FixedPerform()
        {
        }

        public override void Perform()
        {
            float distance = ItemResource.GetDistanceToPlayer();
            if (distance <= SourceSO.FollowDistance)
            {
                Debug.Log("Вижу игрока!!");
                StateMachine.ChangeFollowState();
            }
        }
    }
}
