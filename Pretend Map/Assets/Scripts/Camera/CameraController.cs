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

public enum ZoomType
{
    IN,
    OUT
}

public class CameraController : MonoBehaviour
{
    #region Serializable Fields

    [Header("Camera Settings")]

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private float _defaultOrthographicSize;

    [SerializeField]
    private bool _isUsingMouseInput;

    [Header("Pan")]
    [SerializeField]
    private float _panSpeed;

    [Header("Zoom")]
    [SerializeField]
    private float _maxZoomOut;
    [SerializeField]
    private float _maxZoomIn;
    [SerializeField]
    private float _zoomSpeed;
    [SerializeField]
    private int _zoomIncrement;

    #endregion

    #region Private Fields

    private CinemachineVirtualCamera _virtualCamera;

    private Vector3 _currentTouchPoint;
    private Vector3 _touchOriginalPosition;

    private Vector3 _targetDirection;
    private Vector3 _smoothVelocity;

    private PanState _panState;
    private Touch _touch;

    private bool _isDragging;
    private bool _isPanning;

    private float _orthographicSize;

    #endregion

    private void OnValidate()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _virtualCamera.m_Lens.OrthographicSize = _defaultOrthographicSize;
        _virtualCamera.transform.position = Vector3.zero;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        //if (_panState == PanState.ENABLED && !_isZooming)
        //    Pan();

        //if (_isZoomEnabled)
        //    CameraZoom();

        CameraZoom();

        if (_isUsingMouseInput)
        {
            //Input Will only work when using mouse
            GetMouseInput();
        }
        else
        {
            //Input Will only work on phone
            GetTouchInput();
        }
    }

    private void Init()
    {
        //_panState = PanState.DISABLED;
        //_isZoomEnabled = false;
        //TouchController.onZooming += EnableZoom;
        //TouchController.onFinishZooming += DisableZoom;
        //TouchController.onPanning += SetPanState;
    }
    private void SetPanState(PanState state, Vector3 originalPosition, Vector3 currentPosition)
    {
        _touchOriginalPosition = _camera.ScreenToWorldPoint(originalPosition);
        _currentTouchPoint = _camera.ScreenToWorldPoint(currentPosition);

        _panState = state;
    }

    #region Touch and Mouse Inputs

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
            
            if (_touch.phase == TouchPhase.Moved)
            {
                //transform touch position in world point
                _currentTouchPoint = _camera.ScreenToWorldPoint(Input.GetTouch(0).position);
                Pan();
            }
        }          
    }

    private void GetMouseInput()
    {
        if (!_isPanning)
            _isDragging = false;

        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;

            //transform mouse position in world point
            _touchOriginalPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0) && _isDragging)
        {
            //transform mouse position in world point
            _currentTouchPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
            Pan();
        }
           
        if(Input.GetMouseButtonUp(0))
            _isPanning = false;
    }

    void OnDrawGizmos()
    {
        // Draw a red wire sphere at the last input position on scene view
        Gizmos.color = Color.red;

        var originalP = _touchOriginalPosition;
        originalP.z = Camera.main.transform.position.z;
        _touchOriginalPosition = originalP;

        Gizmos.DrawWireSphere(_touchOriginalPosition, 2);
    }

    #endregion

    #region Pan

    private void Pan()
    {
        Debug.Log("PAN");

        _isPanning = true;

        //the last mouse position minus the current mouse input when "dragging" will give the direction of the movement done
        _targetDirection = _touchOriginalPosition - _currentTouchPoint;

        Debug.Log("target direction " + _targetDirection);
        //the new position of the virtual camera will be the current virtual position plus the direction calculated
        var targetPosition = _virtualCamera.transform.position + _targetDirection;

        //smooth damp will smoothly make the movement between the last mouse input to the new virtual camera position;
        _virtualCamera.transform.position = Vector3.SmoothDamp(_virtualCamera.transform.position, targetPosition, ref _smoothVelocity, _panSpeed); 
    }

    #endregion

    #region Zoom Callback with finger gestures

    //private void EnableZoom(float zoomScale)
    //{
    //    Debug.Log("enabled Zoom");
    //    _isZoomEnabled = true;
    //    _zoomScale = zoomScale;
    //}

    //private void DisableZoom()
    //{
    //    Debug.Log("Disabled Zoom");
    //    _zoomScale = 0f;
    //    _isZoomEnabled = false;
    //}

    #endregion

    #region Zoom With Keys

    private void CameraZoom()
    {
        if (Input.GetKeyDown(KeyCode.A))
            OrthographicCameraZoom(ZoomType.IN);

        if (Input.GetKeyDown(KeyCode.Z))
            OrthographicCameraZoom(ZoomType.OUT);
    }

    private void OrthographicCameraZoom(ZoomType zoomType)
    {
        _orthographicSize = _virtualCamera.m_Lens.OrthographicSize;

        switch (zoomType)
        {
            case ZoomType.IN:
                _orthographicSize -= _zoomIncrement;
                break;
            case ZoomType.OUT:
                _orthographicSize += _zoomIncrement;
                break;
        }

        Debug.Log("Camera zooming");

        var zoomValue = Math.Clamp(_orthographicSize, _maxZoomIn, _maxZoomOut);
        _virtualCamera.m_Lens.OrthographicSize = zoomValue;
    }

    #endregion

}
