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
        public Vector3Int PlacePosition { get; private set; }
        protected bool _isInitialized;
        protected PlacementItemData _data;

        [Inject]
        public virtual void ConstructItem(
            PlacementMapsContainer placementMapsContainer,
            GameDataState gameDataState)
        {
            PlacementMap = placementMapsContainer.ItemPlacementMap;
            GameDataState = gameDataState;
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

            PlacePosition = PlacementMap
            .Vector3ConvertToVector3Int(gameObject.transform.position);
            PlacementMap.PlaceObjectOnCell(gameObject, PlacePosition);
            OccupyCells();
        }
        private void OccupyCells()
        {

            if (this is IOccupyingSeveralCells)
            {
                IOccupyingSeveralCells obj =
                    (this as IOccupyingSeveralCells);
                PlacementMap.AddPositions(obj);
                return;
            }
            if (this is IOccupyingOneCell)
            {
                IOccupyingOneCell obj =
                    (this as IOccupyingOneCell);
                PlacementMap.AddPosition(obj);
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
        }
        public virtual PlacementItemData GetData()
        {
            PlacementItemData data = new PlacementItemData();
            data.SetPosition(PlacePosition);
            return data;
        } 
    }
}
