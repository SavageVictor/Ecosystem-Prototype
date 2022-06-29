using System;
using System.Collections;
using System.Collections.Generic;
using SpawnUser;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Camera generalCamera;
    [SerializeField] private List<WorldTile> _tileDatas;

    [SerializeField] GameObject _iniPlant;

    private Dictionary<TileBase, WorldTile> _dataFromTiles;

    public bool checkInfoMode = false;
    public bool spawnMode = false;

    [SerializeField] private Vector2 spawnAreaMIN;
    [SerializeField] private Vector2 spawnAreaMAX;

    [SerializeField] float timeToSpawn;
    [SerializeField] float timeToSpawnMAX;
    [SerializeField] float timeToSpawnDecrease;
    [SerializeField] float timeToSpawnDecreaseAdded;
    [SerializeField] float timeToDecrease;
    [SerializeField] float timeToDecreaseMAX = 50;
    [SerializeField] private float timeToSpawnMIN = 6;

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
    
    private void Start()
    {
        TimeTickSys.OnTick += TimeTickSys_OnTick;
    }


    private void FixedUpdate()
    {
        if (timeToSpawn >= timeToSpawnMAX - timeToSpawnDecrease)
        {
            ObjRandomSpawn();
            timeToSpawn = 0;
        }

        if (timeToDecrease >= timeToDecreaseMAX && timeToSpawnMAX - timeToSpawnDecrease >= timeToSpawnMIN)
        {
            timeToSpawnDecrease += timeToSpawnDecreaseAdded;
            timeToDecrease = 0;
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
    
    private void ObjRandomSpawn()
    {
        Vector2 plantCoordSpawn = new Vector2(Random.Range(spawnAreaMIN.x, spawnAreaMAX.x),
            Random.Range(spawnAreaMIN.y, spawnAreaMAX.y));
            
        Vector3Int gridPos = tilemap.WorldToCell(plantCoordSpawn);
        if (TileIsPassable(gridPos))
        {
            Instantiate(_iniPlant, plantCoordSpawn , Quaternion.identity);
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
    
    private void TimeTickSys_OnTick(object sender, TimeTickSys.OnTickEventsArgs e)
    {
        timeToSpawn++;
        timeToDecrease++;
    }
}
