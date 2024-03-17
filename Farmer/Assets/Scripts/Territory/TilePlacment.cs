using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacement : MonoBehaviour
{
    public Tilemap tilemap; // ������ �� �������, � ������� �� ���������
    public RuleTile ruleTile; // �������� RuleTile, ������� �� ������ ����������

    void Update()
    {
        // ���������, ���� �� ������ ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("������ ���� ������!");

            // �������� ������� ������� � ������� �����������
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.forward, Vector3.zero);
            float distance;

            if (plane.Raycast(ray, out distance))
            {
                Vector3 worldPos = ray.GetPoint(distance);
                Debug.Log("������� ����������: " + worldPos); // ���������� �����

                // ����������� ������� � ���������� ������ �����
                Vector3Int cellPos = tilemap.WorldToCell(worldPos);
                Debug.Log("���������� ������: " + cellPos); // ���������� �����

                // ������������� �������� RuleTile � ��������� ������
                tilemap.SetTile(cellPos, ruleTile);
            }
        }
    }
}
