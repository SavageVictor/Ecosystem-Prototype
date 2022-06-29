using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantScriptableObj : MonoBehaviour
{
    public List<Sprite> plantGrowthStages;
    public int ticksToStage = 200;
    //public Transform initialPlantPrefab;

    private int _currentStage;
    //private bool _isGrowing;
    
    private int _growingTime = 0;

    private SpriteRenderer _spriteRenderer;

    private GameObject _mapManagerGameObject;
    private MapManager _mapManagerInstance;
    private GameObject _gameManagerGameObject;
    private GameManager _gameManagerInstance;

    private ScriptableObject _scriptableObjectToTurnOff;

    [SerializeField] private int pointsReceived = 10;

    

    private void Start()
    {
        //_isGrowing = true;
        TimeTickSys.OnTick += TimeTickSys_OnTick;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = plantGrowthStages[_currentStage];

        _mapManagerGameObject = GameObject.Find("MapManager");
        _mapManagerInstance = _mapManagerGameObject.GetComponent<MapManager>();

        _gameManagerGameObject = GameObject.Find("GameManager");
        _gameManagerInstance = _gameManagerGameObject.GetComponent<GameManager>();

//        _scriptableObjectToTurnOff = GetComponent<ScriptableObject>();
    }

    private void FixedUpdate()
    {
        if (_growingTime >= ticksToStage && _currentStage < plantGrowthStages.Count - 1 )
        {
            _currentStage++;
            _growingTime = 0;
            _spriteRenderer.sprite = plantGrowthStages[_currentStage];
        }

        if (_currentStage == plantGrowthStages.Count - 1)
        {
            _gameManagerInstance.plantFullyGrown = true;
        }
    }
    
    private void OnMouseDown()
    {
        _gameManagerInstance.score += pointsReceived;
    }
    
    private void TimeTickSys_OnTick(object sender, TimeTickSys.OnTickEventsArgs e)
    {
        _growingTime++;
    }
}