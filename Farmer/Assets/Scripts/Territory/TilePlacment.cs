using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacement : MonoBehaviour
{
    public Tilemap tilemap; // Ссылка на тайлмап, с которым вы работаете
    public RuleTile ruleTile; // Заданный RuleTile, который вы хотите установить

    void Update()
    {
        // Проверяем, была ли нажата кнопка мыши
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Кнопка мыши нажата!");

            // Получаем позицию курсора в мировых координатах
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.forward, Vector3.zero);
            float distance;
            
            if (plane.Raycast(ray, out distance))
            {
                
                Vector3 worldPos = ray.GetPoint(distance);
                Debug.Log("Мировые координаты: " + worldPos); // Отладочный вывод

                // Преобразуем позицию в координаты ячейки сетки
                Vector3Int cellPos = tilemap.WorldToCell(worldPos);
                Debug.Log("Координаты ячейки: " + cellPos); // Отладочный вывод
                if(!tilemap.HasTile(cellPos))
                    {
                // Устанавливаем заданный RuleTile в выбранную ячейку
                tilemap.SetTile(cellPos, ruleTile);

                }
                else
                {
                    Debug.Log("Здесь уже есть тайл!");
                }
            }
        }
    }
}
