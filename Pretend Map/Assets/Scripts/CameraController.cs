using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using System;

public class CameraController : MonoBehaviour
{
    #region Serializable Fields

    [Header("Camera Settings")]

    [SerializeField]
    private Camera _camera;

    [Header("Pan")]
    [SerializeField]
    private float _panSpeed;

    [Header("Screen Edges")]
    [SerializeField]
    private float _rightXEdge;
    [SerializeField]
    private float _leftXEdge;
    [SerializeField]
    private float _upYEdge;
    [SerializeField]
    private float _downYEdge;

    [Header("Zoom")]
    [SerializeField]
    private float _maxZoomOut;
    [SerializeField]
    private float _maxZoomIn;
    [SerializeField]
    private float _zoomIncrement;

    #endregion

    #region Private Fields

    private CinemachineVirtualCamera _virtualCamera;

    private Vector3 _currentTouchPoint;
    private Vector3 _touchOriginPoint;

    private Vector3 _targetDirection;
    private Vector3 _smoothVelocity;

    private bool _isDragging;
    private bool _isCameraPanning;
    private float _orthographicSize;

    #endregion

    private void OnValidate()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _virtualCamera.m_Lens.OrthographicSize = _maxZoomOut;
    }

    void Start()
    {       
        
    }

    void Update()
    {
        CameraZoom();

        GetInput();
    }

    private void GetInput()
    {
        if(!_isCameraPanning)
            _isDragging = false;

        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;

            //transform mouse position in world point
            _touchOriginPoint = _camera.ScreenToWorldPoint(Input.mousePosition);         
        }

        //isOnBounderiesEdges();

        //mouse or touch is being held
        if (Input.GetMouseButton(0) && _isDragging)
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

    #region Pan

    private void Pan()
    {
        _isCameraPanning = true;

        //transform mouse position in world point
        _currentTouchPoint = _camera.ScreenToWorldPoint(Input.mousePosition);

        //the last mouse position minus the current mouse input when "dragging" will give the direction of the movement done
        _targetDirection = _touchOriginPoint - _currentTouchPoint;

        //the new position of the virtual camera will be the current virtual position plus the direction calculated
        var targetPosition = _virtualCamera.transform.position + _targetDirection;

        //smooth damp will smoothly make the movement between the last mouse input to the new virtual camera position;
        _virtualCamera.transform.position = Vector3.SmoothDamp(_virtualCamera.transform.position, targetPosition, ref _smoothVelocity, _panSpeed); 
    }

    private void isOnBounderiesEdges()
    {
        Vector3 _edges = _virtualCamera.transform.position;

        if (_virtualCamera.transform.position.x <= _leftXEdge)
            _edges.x = _leftXEdge;

        if (_virtualCamera.transform.position.x >= _rightXEdge)
            _edges.x = _rightXEdge;

        if (_virtualCamera.transform.position.y >= _upYEdge)
            _edges.y = _upYEdge;

        if (_virtualCamera.transform.position.y <= _downYEdge)
            _edges.y = _downYEdge;

        _virtualCamera.transform.position = _edges;
    }

    #endregion 

    #region Zoom

    private void CameraZoom()
    {
        if (Input.GetKeyDown(KeyCode.A))
            ZoomIn();

        if (Input.GetKeyDown(KeyCode.Z))
            ZoomOut();
    }

    private void ZoomIn()
    {
        _orthographicSize = _virtualCamera.m_Lens.OrthographicSize;
        _orthographicSize -= _zoomIncrement;

        var zoomIn = Math.Clamp(_orthographicSize, _maxZoomIn, _maxZoomOut);
        _virtualCamera.m_Lens.OrthographicSize = zoomIn;

    }

    private void ZoomOut()
    {
        _orthographicSize = _virtualCamera.m_Lens.OrthographicSize;
        _orthographicSize += _zoomIncrement;

        var zoomOut = Math.Clamp(_orthographicSize, _maxZoomIn, _maxZoomOut);
        _virtualCamera.m_Lens.OrthographicSize = zoomOut;
    }

    #endregion
}
