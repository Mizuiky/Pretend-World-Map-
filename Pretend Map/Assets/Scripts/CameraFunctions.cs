using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using System;

public enum PanState
{
    ENABLED,
    DISABLED
}

public class CameraFunctions : MonoBehaviour
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
    private Vector3 _touchOriginalPosition;

    private Vector3 _targetDirection;
    private Vector3 _smoothVelocity;

    private float _orthographicSize;

    private PanState _panState;
    private Touch _touch;

    #endregion

    private void OnValidate()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _virtualCamera.m_Lens.OrthographicSize = _maxZoomOut;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        //if (_panState == PanState.ENABLED)
        //    Pan();

        //CameraZoom();

        GetTouchInput();
    }

    private void Init()
    {
        _panState = PanState.DISABLED;
        //TouchController.onPanning += SetPanState;
    }
    private void SetPanState(PanState state, Vector3 originalPosition, Vector3 currentPosition)
    {
        _touchOriginalPosition = _camera.ScreenToWorldPoint(originalPosition);
        _currentTouchPoint = _camera.ScreenToWorldPoint(currentPosition);

        _panState = state;
    }

    private void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                //transform touch position in world point
                _touchOriginalPosition = _camera.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            else if (_touch.phase == TouchPhase.Moved)
            {
                //transform touch position in world point
                _currentTouchPoint = _camera.ScreenToWorldPoint(Input.GetTouch(0).position);
                Pan();
            }
        }          
    }

    void OnDrawGizmos()
    {
        // Draw a red wire sphere at the last mouse position on scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_touchOriginalPosition, 2);
    }

    #region Pan

    private void Pan()
    {
        //the last mouse position minus the current mouse input when "dragging" will give the direction of the movement done
        _targetDirection = _touchOriginalPosition - _currentTouchPoint;

        //the new position of the virtual camera will be the current virtual position plus the direction calculated
        var targetPosition = _virtualCamera.transform.position + _targetDirection;

        //smooth damp will smoothly make the movement between the last mouse input to the new virtual camera position;
        _virtualCamera.transform.position = Vector3.SmoothDamp(_virtualCamera.transform.position, targetPosition, ref _smoothVelocity, _panSpeed * Time.deltaTime); 
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
