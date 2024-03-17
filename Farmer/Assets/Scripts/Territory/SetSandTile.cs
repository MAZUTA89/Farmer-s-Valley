using Scripts.Territory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class SetSandTile : MonoBehaviour
{
    public Tilemap TilemapSandLayer;
    public RuleTile rule;
    TileClicker tileClicker;
    // Start is called before the first frame update
    void Start()
    {
        tileClicker = new TileClicker(TilemapSandLayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var pos = tileClicker.GetClickedTileVector3Int();
            Debug.Log(pos);
            TilemapSandLayer.SetTile(pos, rule);
        }
    }
}
