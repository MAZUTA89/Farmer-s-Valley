using Scripts.PlayerCode;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Scripts.PlacementCode
{
    [RequireComponent(typeof(Player))]
    public class MarkerController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _interactMarketSprite;
        private Grid _grid;

        Player _player;
        public InteractMarker InteractMarker { get; private set; }
        public Vector3Int CurrentTarget {  get; private set; }  

        [Inject]
        public void Construct(Grid grid)
        {
            _grid = grid;
        }

        private void Start()
        {
            InteractMarker = _interactMarketSprite.GetComponent<InteractMarker>();
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            var currentCell = _grid.WorldToCell(transform.position);
            var pointedCell = _grid.WorldToCell(_player.CurrentWorldMousePos);

            currentCell.z = 0;
            pointedCell.z = 0;

            var toTarget = pointedCell - currentCell;

            if (Mathf.Abs(toTarget.x) > 1)
            {
                toTarget.x = (int)Mathf.Sign(toTarget.x);
            }

            if (Mathf.Abs(toTarget.y) > 1)
            {
                toTarget.y = (int)Mathf.Sign(toTarget.y);
            }

            CurrentTarget = currentCell + toTarget;

            InteractMarker.transform.position =
                _grid.GetCellCenterWorld(CurrentTarget);
        }

    }
}
