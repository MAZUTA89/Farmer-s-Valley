using System.Collections.Generic;

namespace Scripts.PlacementCode
{
    public class PlacementData
    {
        public List<TilePlacementData> CropsTilePlacementDatas;
        public List<PlacedCropData> PlacedCropDatas;
        public List<TilePlacementData> GroundTilePlacementDatas;
        public List<PlacementService.GroundData> GroupDatas;
       
        public PlacementData()
        {
            CropsTilePlacementDatas = new List<TilePlacementData>();
            PlacedCropDatas = new List<PlacedCropData>();
            GroundTilePlacementDatas = new List<TilePlacementData>();
            GroupDatas = new List<PlacementService.GroundData>();
        }
    }
}
