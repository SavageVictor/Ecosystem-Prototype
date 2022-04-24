using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class WorldTile : ScriptableObject
{
    //[SerializeField] private Tilemap worldTileMap;
    public TileBase[] tiles;

    public bool isPassable;
}
