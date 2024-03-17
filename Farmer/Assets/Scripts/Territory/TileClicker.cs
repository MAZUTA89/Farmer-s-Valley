using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.Territory
{
    public class TileClicker
    {
        Tilemap _targetMap;
        Camera _mainCamera;
        public TileClicker(Tilemap targetMap)
        {
            _targetMap = targetMap;
            _mainCamera = Camera.main;
        }
        
        public Vector3Int GetClickedTileVector3Int()
        {
            Vector3 mouseWorldPosition = 
                _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
                Input.mousePosition.y, _mainCamera.transform.position.z));
            //Debug.Log(mouseWorldPosition);
            mouseWorldPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0f);
            return _targetMap.WorldToCell(mouseWorldPosition);
        }

        TileBase GetClickedTile()
        {
            Vector3 mouseWorldPosition =
                _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePos = _targetMap.WorldToCell(mouseWorldPosition);
            return _targetMap.GetTile(tilePos);
        }
    }
}
