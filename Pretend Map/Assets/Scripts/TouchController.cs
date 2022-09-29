using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
using System.Linq;

public class TouchController : MonoBehaviour
{
    #region Gestures

    private TapGestureRecognizer _tapGesture;
    private TapGestureRecognizer _doubleTapGesture;
    private GestureRecognizer _panGesture;
    private ScaleGestureRecognizer _zoomGesture;

    #endregion

    private List<GestureTouch> _touches;
    private Vector3 _touchPoint;
    private Vector3 _touchPosition;

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
        CreatePanGesture();
        CreateZoomGesture();

        //single tap gesture requires that the double tap gesture fail
        _tapGesture.RequireGestureRecognizerToFail = _doubleTapGesture;

        //cant do at the same time pan and scale gestures;
        //_panGesture.DisallowSimultaneousExecution(_zoomGesture);
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
            GestureTouch touch = GetFirstTouch(gesture);
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
            GestureTouch touch = GetFirstTouch(gesture);

            // Raycast to detect game objects on scene
            CheckTargetItem(new Vector2(touch.X, touch.Y));
            DrawTouchPoint(touch.X, touch.Y);

            Debug.Log("DOUBLE TAP GESTURE");
        }
    }

    #endregion

    private void CreatePanGesture()
    {

    }

    private void CreateZoomGesture()
    {

    }

    private GestureTouch GetFirstTouch(GestureRecognizer gesture)
    {
        var currentTouches = gesture.CurrentTrackedTouches;
        var touches = currentTouches.ToList();
        return touches.FirstOrDefault();
    }

    private void CheckTargetItem(Vector3 item)
    {
        _itemPosition = new Vector2(item.x, item.y);

        _ray = Camera.main.ScreenPointToRay(_itemPosition);
        _hit = Physics2D.Raycast(_ray.origin, Vector3.forward);

        if (_hit.collider != null)
        {
            var mapItem = _hit.transform.GetComponent<IMapComponent>();

            if (mapItem != null)
                mapItem.OnDestroyComponent();
        }
    }

    private void DrawTouchPoint(float x, float y)
    {
        _touchPosition = new Vector3(x, y, 0);
    }
}
