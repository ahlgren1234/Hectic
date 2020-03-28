using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    public Vector2 SwipeDelta { get { return swipeDelta;  } }
    public bool Tap { get { return tap; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }


    public GameObject ruta;

    private Camera cam;
    


    private void Start()
    {
        Debug.Log("Width: " + Screen.width + ", Height: " + Screen.height);

        cam = Camera.main;
    }

    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        #region Standalone Inputs

        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }

        #endregion

        #region Mobile Inputs

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                tap = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }

        #endregion


        // Calculate the distance
        swipeDelta = Vector2.zero;

        if (isDraging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }


        // Did we cross the deadzone?
        if (swipeDelta.magnitude > 125)
        {
            // which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Left or Right
                if (x < 0)
                {
                    swipeLeft = true;
                    Debug.Log("Left: " + startTouch.y);
                }
                else
                {
                    swipeRight = true;
                    Debug.Log("Right: " + startTouch.y);
                    

                    Vector3 point = new Vector3();
                    point = cam.ScreenToWorldPoint(new Vector3(startTouch.x, startTouch.y, cam.nearClipPlane));
                    Debug.Log("X: " + point.x.ToString() + ", Y: " + point.y.ToString());

                    Instantiate(ruta, new Vector3(point.x, point.y, 0), Quaternion.identity);
                }
            }
            else
            {
                // Up or Down
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }

            Reset();
        }


    }


    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
