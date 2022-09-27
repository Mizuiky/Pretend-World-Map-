using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]

    [SerializeField]
    private Camera _camera;

    [Header("Pan")]
    [SerializeField]
    private float _smoothTime;

    [SerializeField]
    private float _xBounds;
    [SerializeField]
    private float _yBounds;

    [Header("Zoom")]
    [SerializeField]
    private float _maxZoom;
    [SerializeField]
    private float _minZoom;
    [SerializeField]
    private float _zoomNumber;

    private CinemachineVirtualCamera _virtualCamera;

    private Vector3 _currentTouchPoint;
    private Vector3 _touchOriginPoint;

    private Vector3 _targetDirection;
    private Vector3 _smoothVelocity;

    private bool _isDragging;
    private bool _isCameraPanning;

    private float _currentCameraZoomValue;

    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if(!_isCameraPanning)
            _isDragging = false;
          
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            _touchOriginPoint = _camera.ScreenToWorldPoint(Input.mousePosition);         
        }

        //mouse or touch is being held
        if(Input.GetMouseButton(0) && _isDragging)
            Pan();      

        if (Input.GetMouseButtonUp(0))
            _isCameraPanning = false;
    }

    void OnDrawGizmos()
    {
        // Draw a red wire sphere at the last mouse position on scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_touchOriginPoint, 2);
    }

    private void Pan()
    {
        _isCameraPanning = true;

        _currentTouchPoint = _camera.ScreenToWorldPoint(Input.mousePosition);

        _targetDirection = _touchOriginPoint - _currentTouchPoint;

        var targetPosition = _virtualCamera.transform.position + _targetDirection;

        _virtualCamera.transform.position = Vector3.SmoothDamp(_virtualCamera.transform.position, targetPosition, ref _smoothVelocity, _smoothTime); 
    }

    private void ZoomIn()
    {

    }

    private void ZoomOut()
    {

    }

    private bool isOnBounderiesEdges()
    {
        return true;
    }
}
