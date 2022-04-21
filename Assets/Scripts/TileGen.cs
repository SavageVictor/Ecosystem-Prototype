using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGen : MonoBehaviour
{
    [SerializeField] private RuleTile dirt;
    [SerializeField] private Tilemap ground;

    private Vector3Int _vector3Int = Vector3Int.zero;
    
    void Start()
    {
        ground.FloodFill(_vector3Int, dirt);
    }
}
