using System;
using System.Collections.Generic;
using Scripts.StateMachine;

namespace Scripts.InventoryCode.ItemResources
{
    public class ItemResourceStateMachine : UniversalStateMachine<ItemResourceState>
    {
        ItemResource _itemResource;
        public ItemResourceStateMachine(ItemResource itemResource)
        {
            _itemResource = itemResource;
        }
        public void Perform()
        {
            CurrentState.Perform();
        }
        public void FixedPerform()
        {
            CurrentState.FixedPerform();
        }
        public void ChangeFollowState()
        {
            ChangeState(_itemResource.FollowState);
        }
        public void ChangePushSate()
        {
            ChangeState(_itemResource.PushState);
        }
        public void ChangeOnGroundState()
        {
            ChangeState(_itemResource.OnGroundState);
        }
    }
}
