using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera generalCamera;

    [Range(1,100)]
    [SerializeField] private int scrollSpeed = 50;

    private void LateUpdate()
    {
        generalCamera.orthographicSize -= Input.mouseScrollDelta.y * Time.deltaTime * scrollSpeed;
    }
}
