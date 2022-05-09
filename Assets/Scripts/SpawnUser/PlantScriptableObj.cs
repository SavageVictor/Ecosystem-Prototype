using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantScriptableObj : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase grassTile;
    
    public List<Sprite> plantGrowthStages;
    public int ticksToStage = 200;
    //public Transform initialPlantPrefab;

    private int _currentStage;
    //private bool _isGrowing;
    
    private int _growingTime = 0;
    
    int distance = 0;

    private SpriteRenderer _spriteRenderer;

    private GameObject _mapManagerGameObject;
    private MapManager _mapManagerInstance;

    private void Start()
    {
        //_isGrowing = true;
        TimeTickSys.OnTick += TimeTickSys_OnTick;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = plantGrowthStages[_currentStage];

        GameObject tilemapGameObj = GameObject.Find("Ground");
        _tilemap = tilemapGameObj.GetComponent<Tilemap>();

        _mapManagerGameObject = GameObject.Find("MapManager");
        _mapManagerInstance = _mapManagerGameObject.GetComponent<MapManager>();
        //SetTiles();
    }

    private void Update()
    {
        if (_growingTime >= ticksToStage && _currentStage < plantGrowthStages.Count - 1 )
        {
            _currentStage++;
            _growingTime = 0;
            _spriteRenderer.sprite = plantGrowthStages[_currentStage];
        }
    }

    private void SetTiles()
    {
        
        Vector3Int gridPos = _tilemap.WorldToCell(transform.position);
        
        distance++;
        int max_distance = 3;
        for (int i = gridPos.x - distance; i < gridPos.x + distance; i++)
        {
            for (int j = gridPos.y - distance; j < gridPos.y + distance; j++)
            {
                if((Mathf.Abs(gridPos.x - i) + Mathf.Abs(gridPos.y - j)) <= distance && distance <= max_distance)
                {

                    if (_mapManagerInstance.TileIsPassable(new Vector3Int(i, j, 0)))
                    {
                        _tilemap.SetTile(new Vector3Int(i,j,0), grassTile);
                    
                        Debug.Log(new Vector2(i,j));
                    }
                    //TileBase currentTileBase = _tilemap.GetTile(new Vector3Int(i,j,0));
                }
            }
        }
    }


    private void TimeTickSys_OnTick(object sender, TimeTickSys.OnTickEventsArgs e)
    {
        _growingTime++;
        SetTiles();
    }
}