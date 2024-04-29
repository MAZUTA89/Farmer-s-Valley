using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.InteractableObjects
{
    internal class SandFactory : ISandFactory
    {
        Tilemap _sandTileMap;
        RuleTile _sandTile;
        public SandFactory(Tilemap sandTilemap, RuleTile sandTile)
        {
            _sandTileMap = sandTilemap;
            _sandTile = sandTile;
        }
        public RuleTile Create(Vector3Int createData)
        {
            _sandTileMap.SetTile(createData, _sandTile);
            return _sandTile;
        }
    }
}
