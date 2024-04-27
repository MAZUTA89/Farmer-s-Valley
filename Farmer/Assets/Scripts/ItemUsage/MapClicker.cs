using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

        public virtual bool IsClicked(out Vector3Int clickedPosition)
        {
            if (_inputService.IsRBK())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(Vector3.forward, Vector3.zero);
                float distance;

                if (plane.Raycast(ray, out distance))
                {
                    Vector3 worldPos = ray.GetPoint(distance);
                    // Преобразуем позицию в координаты ячейки сетки
                    clickedPosition = _tileMap.WorldToCell(worldPos);
                    Debug.Log("Координаты ячейки: " + clickedPosition); // Отладочный вывод
                    return true;
                }
                else
                {
                    clickedPosition = Vector3Int.zero;
                    return false;
                }
            }
            else 
            {
                clickedPosition = Vector3Int.zero;
                return false;
            }
        }
    }
}
