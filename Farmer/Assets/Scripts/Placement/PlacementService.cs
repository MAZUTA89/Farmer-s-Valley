using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.VFX;
using Zenject;
using Crop = Scripts.InventoryCode.Crop;


namespace Scripts.PlacementCode
{
    public class PlacementService : MonoBehaviour
    {
        static PlacementService s_instance;
        Grid _grid;
        CropDataBase _cropDatabase;
        [Inject]
        public void Construct(Grid grid,
            CropDataBase cropDataBase
            )
        {
            _grid = grid;
            _cropDatabase = cropDataBase;
        }

        public Tilemap GroundTilemap;
        public Tilemap CropTilemap;

        [Header("Watering")]
        public Tilemap WaterTilemap;
        public TileBase WateredTile;

        [Header("Tilling")]
        public TileBase TilleableTile;
        public TileBase TilledTile;
        public VisualEffect TillingEffectPrefab;

        private Dictionary<Crop, List<VisualEffect>> m_HarvestEffectPool = new();
        private List<VisualEffect> _tillingEffectPool = new();

        public class GroundData
        {
            public const float WaterDuration = 60 * 1.0f;

            public float WaterTimer;
        }
        public class CropData
        {
            [Serializable]
            public struct SaveData
            {
                public string CropId;
                public int Stage;
                public float GrowthRatio;
                public float GrowthTimer;
                public int HarvestCount;
                public float DyingTimer;
            }

            public Crop GrowingCrop = null;
            public int CurrentGrowthStage = 0;

            public float GrowthRatio = 0.0f;
            public float GrowthTimer = 0.0f;

            public int HarvestCount = 0;

            public float DyingTimer;
            public bool HarvestDone => HarvestCount == GrowingCrop.NumberOfHarvest;

            public void Init()
            {
                GrowingCrop = null;
                GrowthRatio = 0.0f;
                GrowthTimer = 0.0f;
                CurrentGrowthStage = 0;
                HarvestCount = 0;

                DyingTimer = 0.0f;
            }

            public Crop Harvest()
            {
                var crop = GrowingCrop;

                HarvestCount += 1;

                CurrentGrowthStage = GrowingCrop.StageAfterHarvest;
                GrowthRatio = CurrentGrowthStage / (float)GrowingCrop.GrowthStagesTiles.Length;
                GrowthTimer = GrowingCrop.GrowthTime * GrowthRatio;

                return crop;
            }

            public void Save(ref SaveData data)
            {
                data.Stage = CurrentGrowthStage;
                data.CropId = GrowingCrop.UniqueName;
                data.DyingTimer = DyingTimer;
                data.GrowthRatio = GrowthRatio;
                data.GrowthTimer = GrowthTimer;
                data.HarvestCount = HarvestCount;
            }

            public void Load(SaveData data, 
                CropDataBase cropDataBase)
            {
                CurrentGrowthStage = data.Stage;
                GrowingCrop = cropDataBase.GetItemByName(data.CropId);
                DyingTimer = data.DyingTimer;
                GrowthRatio = data.GrowthRatio;
                GrowthTimer = data.GrowthTimer;
                HarvestCount = data.HarvestCount;
            }
        }


        private Dictionary<Vector3Int, GroundData> _groundData = new();
        private Dictionary<Vector3Int, CropData> _cropData = new();

        private void Awake()
        {
            if(s_instance == null)
            {
                s_instance = this;
            }
        }

        private void Start()
        {
            for (int i = 0; i < 4; ++i)
            {
                var effect = Instantiate(TillingEffectPrefab);
                effect.gameObject.SetActive(true);
                effect.Stop();
                _tillingEffectPool.Add(effect);
            }
        }

        private void Update()
        {
            foreach (var (cell, groundData) in _groundData)
            {
                if (groundData.WaterTimer > 0.0f)
                {
                    groundData.WaterTimer -= Time.deltaTime;

                    if (groundData.WaterTimer <= 0.0f)
                    {
                        WaterTilemap.SetTile(cell, null);
                        //GroundTilemap.SetColor(cell, Color.white);
                    }
                }

                if (_cropData.TryGetValue(cell, out var cropData))
                {
                    if (groundData.WaterTimer <= 0.0f)
                    {
                        cropData.DyingTimer += Time.deltaTime;
                        if (cropData.DyingTimer > cropData.GrowingCrop.DryDeathTimer)
                        {
                            _cropData.Remove(cell);
                            UpdateCropVisual(cell);
                        }
                    }
                    else
                    {
                        cropData.DyingTimer = 0.0f;
                        cropData.GrowthTimer = Mathf.Clamp(cropData.GrowthTimer + Time.deltaTime, 0.0f,
                            cropData.GrowingCrop.GrowthTime);
                        cropData.GrowthRatio = cropData.GrowthTimer / cropData.GrowingCrop.GrowthTime;
                        int growthStage = cropData.GrowingCrop.GetGrowthStage(cropData.GrowthRatio);

                        if (growthStage != cropData.CurrentGrowthStage)
                        {
                            cropData.CurrentGrowthStage = growthStage;
                            UpdateCropVisual(cell);
                        }
                    }
                }
            }
        }

        public bool IsTillable(Vector3Int target)
        {
            return GroundTilemap.GetTile(target) == TilleableTile;
        }

        public bool IsPlantable(Vector3Int target)
        {
            return IsTilled(target) && ! _cropData.ContainsKey(target);
        }

        public bool IsTilled(Vector3Int target)
        {
            return _groundData.ContainsKey(target);
        }

        public void TillAt(Vector3Int target)
        {
            if (IsTilled(target))
                return;

            GroundTilemap.SetTile(target, TilledTile);
            _groundData.Add(target, new GroundData());

            var inst = _tillingEffectPool[0];
            _tillingEffectPool.RemoveAt(0);
            _tillingEffectPool.Add(inst);

            inst.gameObject.transform.position = _grid.GetCellCenterWorld(target);

            inst.Stop();
            inst.Play();
        }

        public void PlantAt(Vector3Int target, Crop cropToPlant)
        {
            var cropData = new CropData();

            cropData.GrowingCrop = cropToPlant;
            cropData.GrowthTimer = 0.0f;
            cropData.CurrentGrowthStage = 0;

            _cropData.Add(target, cropData);

            UpdateCropVisual(target);

            if (!m_HarvestEffectPool.ContainsKey(cropToPlant))
            {
                InitHarvestEffect(cropToPlant);
            }
        }
        public Crop HarvestAt(Vector3Int target)
        {
            _cropData.TryGetValue(target, out var data);

            if (data == null || !Mathf.Approximately(data.GrowthRatio, 1.0f)) return null;

            var produce = data.Harvest();

            if (data.HarvestDone)
            {
                _cropData.Remove(target);
            }

            UpdateCropVisual(target);

            var effect = m_HarvestEffectPool[data.GrowingCrop][0];
            effect.transform.position = _grid.GetCellCenterWorld(target);
            m_HarvestEffectPool[data.GrowingCrop].RemoveAt(0);
            m_HarvestEffectPool[data.GrowingCrop].Add(effect);
            effect.Play();

            return produce;
        }

        public void WaterAt(Vector3Int target)
        {
            var groundData = _groundData[target];

            groundData.WaterTimer = GroundData.WaterDuration;
            
            WaterTilemap.SetTile(target, WateredTile);
            //GroundTilemap.SetColor(target, WateredTiledColorTint);
        }

        void UpdateCropVisual(Vector3Int target)
        {
            if (!_cropData.TryGetValue(target, out var data))
            {
                CropTilemap.SetTile(target, null);
            }
            else
            {
                CropTilemap.SetTile(target, data.GrowingCrop.GrowthStagesTiles[data.CurrentGrowthStage]);
            }
        }
        public CropData GetCropDataAt(Vector3Int target)
        {
            _cropData.TryGetValue(target, out var data);
            return data;
        }
        public void InitHarvestEffect(Crop crop)
        {
            m_HarvestEffectPool[crop] = new List<VisualEffect>();
            for (int i = 0; i < 4; ++i)
            {
                var inst = Instantiate(crop.PickEffect);
                inst.Stop();
                m_HarvestEffectPool[crop].Add(inst);
            }
        }

        public static PlacementService Instance()
        {
            return s_instance;
        }
    }
}
