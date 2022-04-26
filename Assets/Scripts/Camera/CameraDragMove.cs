using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraDragMove : MonoBehaviour
{
    private Vector3 _origin;
    private Vector3 _difference;

    private bool _isDragged = false;

    [SerializeField] private Camera generalCamera;

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            _difference = generalCamera.ScreenToWorldPoint(Input.mousePosition) - generalCamera.transform.position;
            if (_isDragged == false)
            {
                _isDragged = true;
                _origin = generalCamera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            _isDragged = false;
        }

        if (_isDragged == true)
        {
            generalCamera.transform.position = _origin - _difference;
        }
        
        // Debug.Log(generalCamera.ScreenToWorldPoint(Input.mousePosition));
    }
}
