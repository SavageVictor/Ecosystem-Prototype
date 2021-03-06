using System;
using System.Collections;
using System.Collections.Generic;
using SpawnUser;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Camera generalCamera;
    [SerializeField] private List<WorldTile> _tileDatas;
    
    [SerializeField] GameObject _iniPlant;

    private Dictionary<TileBase, WorldTile> _dataFromTiles;

    public bool checkInfoMode = false;
    public bool spawnMode = false;

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
        Vector2 mousePos = generalCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPos = tilemap.WorldToCell(mousePos);
        TileBase clickedTileBase = tilemap.GetTile(gridPos);
        //TileBase clickedTileBase = tilemap.GetTile(gridPos);

        //bool isPassable = _dataFromTiles[clickedTileBase].isPassable;
        
        if (Input.GetMouseButtonDown(0) && checkInfoMode)
        {
            print( clickedTileBase + "is passable:" + TileIsPassable(gridPos));
        }
        
        if (Input.GetMouseButtonDown(0) && spawnMode && TileIsPassable(gridPos))
        {
            Instantiate(_iniPlant, mousePos, Quaternion.identity);
        }
    }

    public bool TileIsPassable(Vector3Int tileCoords)
    {
        TileBase neededTile = tilemap.GetTile(tileCoords);
        bool isPassable = _dataFromTiles[neededTile].isPassable;
        return isPassable;
    }

    public void SpawnModeToggle()
    {
        spawnMode = !spawnMode;
    }
    public void CheckInfoModeToggle()
    {
        checkInfoMode = !checkInfoMode;
    }
}
