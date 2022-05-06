using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTickSys : MonoBehaviour
{
    private const float TickTimeMAX = .05f;

    public class OnTickEventsArgs : EventArgs
    {
        public int _tick;
    }

    public static event EventHandler<OnTickEventsArgs> OnTick;
        
    private float _tickTimer;

    [SerializeField] private TextMeshProUGUI DispInfo;
    [SerializeField] private TextMeshProUGUI TPSdisplay;
    
    private int _tick;

    private float TPS_timer;
    private int TPS;

    private void Awake()
    {
        _tick = 0;
    }

    private void Update()
    {
        _tickTimer += Time.deltaTime;

        if (_tickTimer >= TickTimeMAX)
        {
            _tickTimer -= TickTimeMAX;
            _tick++;

            if (OnTick != null) OnTick(this, new OnTickEventsArgs {_tick = _tick});
            
            TPS_timer++;
            
            DispInfo.text = "Ticks Passed: " + _tick;
        }
        
        
        TPS_timer += Time.deltaTime;
        if (TPS_timer >= 1)
        {
            TPSdisplay.text = "TPS: " + TPS;
            TPS = 0;
            TPS_timer = 0;
        }
        
    }
}
