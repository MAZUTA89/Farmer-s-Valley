﻿using AScripts.SaveLoader;
using Assets.Scripts.Placement;
using Scripts.PlacementCode;
using Scripts.SaveLoader;
using Scripts.SO.InteractableObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.InteractableObjects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Seed : PlacementItem, IInteractable, IOccupyingOneCell
    {
        SpriteRenderer _spriteRenderer;
        protected SeedSO SeedSO;
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
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        protected override void Start()
        {
            base.Start();
           
        }
        public void Initialize(SeedSO seedSO)
        {
            SeedSO = seedSO;
            _growthStages = SeedSO.StagesSpriteList.Count;
            _sprites = SeedSO.StagesSpriteList;
            _intervalTime = SeedSO.IntervalTimeBetweenGrowthStages;
            _spriteRenderer.sprite = _sprites[0];
        }
        public virtual void LoadSeed(SeedData seedData)
        {
           _currentTime = seedData.CurrentTime;
            _currentStage = seedData.GrowthStage;
            _spriteRenderer.sprite = _sprites[_currentStage];
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
        public override PlacementItemData GetData()
        {
            var data = base.GetData();
            SeedData seedData = new SeedData();
            seedData.SetPosition(data.GetPosition());
            seedData.CurrentTime = _currentTime;
            seedData.GrowthStage = _currentStage;
            seedData.SeedSOName = SeedSO.name;
            return seedData;
        }
    }
}
