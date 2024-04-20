using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.Tilemaps;
using Assets.Scripts.Placement;
using Scripts.SaveLoader;
using Scripts.FarmGameEvents;

namespace Scripts.PlacementCode
{
    public abstract class PlacementItem : MonoBehaviour, ISaveLoadPlacementItem
    {
        [SerializeField] private bool _isDefaultStateObject;
        protected ItemPlacementMap PlacementMap;
        protected GameDataState GameDataState;
        public Vector2Int PlacePosition { get; private set; }
        protected bool _isInitialized;
        protected PlacementItemData _data;
        protected bool IsSaved;

        [Inject]
        public void ConstructItem(ItemPlacementMap placementMap,
            GameDataState gameDataState)
        {
            PlacementMap = placementMap;
            GameDataState = gameDataState;
        }

        

        public virtual void InitializePosition(Vector2Int placePosition)
        {
            PlacePosition = placePosition;
            _isInitialized = true;
        }
        public void OnEnable()
        {
            GameEvents.OnExitTheGameEvent += OnExitTheGame;
        }
        private void OnDisable()
        {
            GameEvents.OnExitTheGameEvent -= OnExitTheGame;
        }
        protected virtual void Start()
        {
            if (!_isInitialized)
            {
                PlacePosition = PlacementMap
                .Vector3ConvertToVector2Int(gameObject.transform.position);
            }
            OccupyCells();
        }
        private void OccupyCells()
        {
            if (this is IOccupyingСells)
            {
                if (this is IOccupyingSeveralCells)
                {
                    IOccupyingSeveralCells obj =
                        (this as IOccupyingSeveralCells);
                    PlacementMap.AddPositions(obj);
                }
                if (this is IOccupyingOneCell)
                {
                    IOccupyingOneCell obj =
                        (this as IOccupyingOneCell);
                    PlacementMap.AddPosition(obj);
                }
            }
        }
        public virtual void OnExitTheGame()
        {
            SaveData();
        }

        public virtual void SaveData()
        {
            _data = GetData();
            GameDataState.AddItemData(_data);
            IsSaved = true;
        }
        public virtual PlacementItemData GetData()
        {
            PlacementItemData data = new PlacementItemData();
            data.SetPosition(PlacePosition);
            return data;
        }
    }
}
