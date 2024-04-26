using Assets.Scripts.Placement;
using Scripts.PlacementCode;
using Scripts.SaveLoader;
using Scripts.SO.InteractableObjects;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.InteractableObjects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Seed : PlacementItem, IInteractable, IOccupyingOneCell
    {
        SpriteRenderer _spriteRenderer;
        SeedSO _seedSO;
        float _currentTime;
        int _growthStages;
        List<Sprite> _sprites;
        float _intervalTime;
        int _currentStage;

        public override void ConstructItem(PlacementMapsContainer placementMapsContainer,
            GameDataState gameDataState)
        {
            GameDataState = gameDataState;
            PlacementMap = placementMapsContainer.SeedPlacementMap;
        }
        protected override void Start()
        {
            base.Start();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _growthStages = _seedSO.StagesSpriteList.Count;
            _sprites = _seedSO.StagesSpriteList;
            _intervalTime = _seedSO.IntervalTimeBetweenGrowthStages;
            _spriteRenderer.sprite = _sprites[0];
        }
        public void Initialize(SeedSO seedSO)
        {
            _seedSO = seedSO;
        }
        public void Update()
        {
            if(_currentStage < _growthStages - 1)
            {
                _currentTime += Time.deltaTime;
                if (_currentTime >= _intervalTime)
                {
                    _currentStage++;
                    _spriteRenderer.sprite = _sprites[_currentStage];
                    _currentTime = 0;
                }
            }
        }

        public Vector3Int GetOccupyingCell()
        {
            return PlacePosition;
        }
    }
}
