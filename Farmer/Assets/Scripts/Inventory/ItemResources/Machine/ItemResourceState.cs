using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scripts.StateMachine;
using Unity.VisualScripting;
using UnityEngine;

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
