using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace Scripts.ItemUsage
{
    public class MapClicker
    {
        Tilemap _tileMap;
        InputService _inputService;
        public MapClicker(Tilemap tileMap, InputService inputService)
        {
            _tileMap = tileMap;
            _inputService = inputService;
        }

        public virtual bool IsClicked(out Vector2Int clickedPosition)
        {
            if (_inputService.IsRBK())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(Vector3.forward, Vector3.zero);
                float distance;

                if (plane.Raycast(ray, out distance))
                {
                    Vector3 worldPos = ray.GetPoint(distance);
                    Debug.Log("Мировые координаты: " + worldPos); // Отладочный вывод

                    // Преобразуем позицию в координаты ячейки сетки
                    Vector3Int cellPos = _tileMap.WorldToCell(worldPos);
                    Debug.Log("Координаты ячейки: " + cellPos); // Отладочный вывод

                    clickedPosition = new Vector2Int(cellPos.x, cellPos.y);
                    return true;
                }
                else
                {
                    clickedPosition = Vector2Int.zero;
                    return false;
                }
            }
            else 
            {
                clickedPosition = Vector2Int.zero;
                return false;
            }
        }
    }
}
