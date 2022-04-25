using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap[] tilemap;
    [SerializeField] private Camera generalCamera;
    [SerializeField] private List<WorldTile> _tileDatas;

    private Dictionary<TileBase, WorldTile> _dataFromTiles;

    private void Awake()
    {
        _dataFromTiles = new Dictionary<TileBase, WorldTile>();
        
        foreach (var tileData in _tileDatas)
        { 
            foreach (var tile in tileData.tiles)
            { 
                _dataFromTiles.Add(tile, tileData);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos = generalCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = tilemap[1].WorldToCell(mousePos);
            TileBase clickedTileBase = tilemap[1].GetTile(gridPos);

            bool isPassable = _dataFromTiles[clickedTileBase].isPassable;
            
            print( clickedTileBase + "is passable:" + isPassable);
        }
    }
}