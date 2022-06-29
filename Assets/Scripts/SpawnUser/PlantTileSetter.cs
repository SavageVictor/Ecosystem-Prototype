using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantTileSetter : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase grassTile;
    [SerializeField] private TileBase dirtTile;
    
    private GameObject _mapManagerGameObject;
    private MapManager _mapManagerInstance;
    
    private int _distance = 0;
    private int _cycle = 0;
    private int _cyclesToDestroy;

    private bool _destroyed;
    
    private void Start()
    {
        TimeTickSys.OnTick += TimeTickSys_OnTick;
        
        GameObject tilemapGameObj = GameObject.Find("Ground");
        _tilemap = tilemapGameObj.GetComponent<Tilemap>();

        _mapManagerGameObject = GameObject.Find("MapManager");
        _mapManagerInstance = _mapManagerGameObject.GetComponent<MapManager>();
    }

    private void SetTiles(TileBase tileToSet)
    {
        
        Vector3Int gridPos = _tilemap.WorldToCell(transform.position);
        
        _distance++;
        int max_distance = 3;
        for (int i = gridPos.x - _distance; i < gridPos.x + _distance; i++)
        {
            for (int j = gridPos.y - _distance; j < gridPos.y + _distance; j++)
            {
                if((Mathf.Abs(gridPos.x - i) + Mathf.Abs(gridPos.y - j)) <= _distance && _distance <= max_distance)
                {

                    if (_mapManagerInstance.TileIsPassable(new Vector3Int(i, j, 0)))
                    {
                        _tilemap.SetTile(new Vector3Int(i,j,0), tileToSet);
                    
                        // Debug.Log(new Vector2(i,j));
                    }
                    //TileBase currentTileBase = _tilemap.GetTile(new Vector3Int(i,j,0));
                }
            }
        }

        _cycle++;
    }
    
    private void CycleActivation()
    {
        if (_cycle < 3 && !_destroyed)
        {
            SetTiles(grassTile);
        }
        else if (_cycle <= 3 && _destroyed)
        {       
            if (_cycle == 3)
            {
                Destroy(gameObject);
            }
            //Debug.Log("Dirt is being set");
            SetTiles(dirtTile);
            _cyclesToDestroy++;
        }
    }
    
    //Destroys object when all tiles are set.
    private void OnMouseDown()
    {
        _destroyed = true;
        _cycle = 0;
        _distance = 0;
    }
    
    private void TimeTickSys_OnTick(object sender, TimeTickSys.OnTickEventsArgs e)
    {
        CycleActivation();
    }
}
