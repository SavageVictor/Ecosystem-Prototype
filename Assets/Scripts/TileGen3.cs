using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TileGen3 : MonoBehaviour
{
    [SerializeField] private Tilemap groundTileMap;
    [SerializeField] private Tile dirt;
    [SerializeField] private Tile water;

    private Vector2 _originOffset;

    private void Start()
    {
        _originOffset.x = Random.Range(15, 130);
        _originOffset.y = Random.Range(15, 130);
    }
}
