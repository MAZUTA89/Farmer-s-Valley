using Scripts.StateMachine;

namespace Scripts.InventoryCode.ItemResources
{
    public abstract class ItemResourceState : IUniversalState
    {
        protected ItemResourceStateMachine StateMachine { get; set; }
        protected ItemResource ItemResource;
        protected ItemSourceSO SourceSO;
        public ItemResourceState(ItemResourceStateMachine universalStateMachine,
            ItemResource itemResource,
            ItemSourceSO sourceSO)
        {
            StateMachine = universalStateMachine;
            ItemResource = itemResource;
            SourceSO = sourceSO;
        }
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Perform();
        public abstract void FixedPerform();
    }
}
