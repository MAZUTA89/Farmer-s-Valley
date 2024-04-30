using Scripts.PlacementCode;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Scripts.ItemUsage
{
    public class ItemApplierTools
    {
        public PlacementMapsContainer PlacementMapsContainer { get; private set; }
        public MapClicker MapClicker { get; private set; }
        public DiContainer DiContainer { get; private set; }
        public ItemApplierTools(PlacementMapsContainer placementMapsContainer,
            MapClicker mapClicker, DiContainer diContainer
            )
        {
            PlacementMapsContainer = placementMapsContainer;
            MapClicker = mapClicker;
            DiContainer = diContainer;
        }
    }
}
