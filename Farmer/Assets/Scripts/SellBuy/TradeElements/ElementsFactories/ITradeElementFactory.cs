using Scripts.InteractableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.SellBuy
{
    public interface ITradeElementFactory : IGameObjectFactory<TradeElement, Transform>
    {
        
    }
}
