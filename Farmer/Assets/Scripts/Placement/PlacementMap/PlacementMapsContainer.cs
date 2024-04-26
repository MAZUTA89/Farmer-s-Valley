using System;
using System.Collections.Generic;


namespace Scripts.PlacementCode
{
    public class PlacementMapsContainer
    {
        public ItemPlacementMap ItemPlacementMap { get; }
        public SandTilePlacementMap SandTilePlacementMap { get; }
        public ItemPlacementMap SeedPlacementMap { get; }
        public PlacementMapsContainer(ItemPlacementMap itemPlacementMap,
            SandTilePlacementMap sandTilePlacementMap,
            ItemPlacementMap seedPlacementMap) 
        {
            ItemPlacementMap = itemPlacementMap;
            SandTilePlacementMap = sandTilePlacementMap;
            SeedPlacementMap = seedPlacementMap;
        }
    }
}
