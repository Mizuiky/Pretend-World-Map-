using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
using System.Linq;
using System;

public class TouchController : MonoBehaviour
{
    [SerializeField]
    private CameraFunctions _camera;

    public static event Action<PanState, Vector3, Vector3> onPanning;
    public static event Action<float> onZooming;
    public static event Action onFinishZooming;

    #region Gestures

    private TapGestureRecognizer _tapGesture;
    private TapGestureRecognizer _doubleTapGesture;
    private PanGestureRecognizer _panGesture;
    private ScaleGestureRecognizer _pinchZoomGesture;

    #endregion

    private List<GestureTouch> _touches;
    private Vector3 _touchPoint;
    private Vector3 _touchPosition;

    private Vector3 _panTouchBegan;
    private Vector3 _panTouchMoved;

    private RaycastHit2D _hit;
    private Ray _ray;
    private Vector2 _itemPosition;

    void Start()
    {
        Init();
    }
    void Update()
    {
        // it will draw the current wold touch point using a blue wire sphere on the scene view
        _touchPoint = Camera.main.ScreenToWorldPoint(_touchPosition);
    }

    void OnDrawGizmos()
    {
        // Draw a red wire sphere at the last touch position on scene view
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_touchPoint, 2);
    }

    private void Init()
    {
        CreateTapGesture();
        CreateDoubleTapGesture();
        //CreatePanGesture();
        CreatePinchZoomGesture();

        //single tap gesture requires that the double tap gesture fail
        _tapGesture.RequireGestureRecognizerToFail = _doubleTapGesture;
    }

    #region Tap Gesture
    private void CreateTapGesture()
    {
        _tapGesture = new TapGestureRecognizer();
        _tapGesture.StateUpdated += TapGestureCallBack;
        FingersScript.Instance.AddGesture(_tapGesture);
    }

    private void TapGestureCallBack(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            GestureTouch touch = GetTouches(gesture).FirstOrDefault();
            DrawTouchPoint(touch.X, touch.Y);

            Debug.Log("ONE TAP GESTURE");
        }
    }

    #endregion

    #region Double Tap Gesture

    private void CreateDoubleTapGesture()
    {
        _doubleTapGesture = new TapGestureRecognizer();
        _doubleTapGesture.NumberOfTapsRequired = 2;
        _doubleTapGesture.StateUpdated += DoubleTapCallBack;
        FingersScript.Instance.AddGesture(_doubleTapGesture);
    }

    private void DoubleTapCallBack(GestureRecognizer gesture)
    {
        if(gesture.State == GestureRecognizerState.Ended)
        {
            GestureTouch touch = GetTouches(gesture).LastOrDefault();

            // Raycast to detect game objects on scene
            SelectTouchedItem(new Vector2(touch.X, touch.Y));
            DrawTouchPoint(touch.X, touch.Y);

            Debug.Log("DOUBLE TAP GESTURE");
        }
    }

    #endregion

    #region  Pan Gesture

    //private void CreatePanGesture()
    //{
    //    _panGesture = new PanGestureRecognizer();
    //    _panGesture.MinimumNumberOfTouchesToTrack = _panGesture.MaximumNumberOfTouchesToTrack = 1;
    //    _panGesture.StateUpdated += PanGestureCallBack;
    //    FingersScript.Instance.AddGesture(_panGesture);
    //}

    //private void PanGestureCallBack(GestureRecognizer gesture)
    //{
    //    Debug.Log("Pan Gesture");
    //    GestureTouch touch = GetFirstTouch(gesture);

    //    if(gesture.State == GestureRecognizerState.Began)
    //    {
    //        _panTouchBegan = new Vector2(touch.X, touch.Y);
    //    }
    //    else if(gesture.State == GestureRecognizerState.Executing)
    //    {
    //        _panTouchMoved = new Vector2(touch.X, touch.Y);

    //        onPanning?.Invoke(PanState.ENABLED, _panTouchBegan, _panTouchMoved);
    //    }
    //    else if(gesture.State == GestureRecognizerState.Ended)
    //    {
    //        onPanning?.Invoke(PanState.DISABLED, _panTouchBegan, _panTouchMoved);
    //    }
    //}

    #endregion

    #region Zoom Gesture

    private void CreatePinchZoomGesture()
    {
        _pinchZoomGesture = new ScaleGestureRecognizer();
        _pinchZoomGesture.MaximumNumberOfTouchesToTrack = _pinchZoomGesture.MinimumNumberOfTouchesToTrack = 2;
        _pinchZoomGesture.StateUpdated += PinchZoomGestureCallBack;
        FingersScript.Instance.AddGesture(_pinchZoomGesture);
    }

    private void PinchZoomGestureCallBack(GestureRecognizer gesture)
    {      
        if (gesture.State == GestureRecognizerState.Executing)
        {
            Debug.Log("Scale mutiplier ");
            onZooming?.Invoke(_pinchZoomGesture.ScaleMultiplier);
        }
        else if(gesture.State == GestureRecognizerState.Ended)
            onFinishZooming.Invoke();
    }

    #endregion

    private List<GestureTouch> GetTouches(GestureRecognizer gesture)
    {
        var currentTouches = gesture.CurrentTrackedTouches;
        var touches = currentTouches.ToList();
        return touches;
    }

    private void SelectTouchedItem(Vector3 item)
    {
        Debug.Log("Select Item");

        _itemPosition = new Vector2(item.x, item.y);

        _ray = Camera.main.ScreenPointToRay(_itemPosition);
        _hit = Physics2D.GetRayIntersection(_ray);

        if (_hit.collider != null)
        {
            Debug.Log("Hit != null");
            var mapItem = _hit.transform.GetComponent<IMapComponent>();

            if (mapItem != null)
            {
                Debug.Log("map != null");
                mapItem.OnDestroyComponent();
            }              
        }
    }

    private void DrawTouchPoint(float x, float y)
    {
        _touchPosition = new Vector3(x, y, Camera.main.transform.position.z);
    }
}
