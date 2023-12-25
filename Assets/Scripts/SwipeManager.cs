using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager instance;

    Vector2 _startTouch;
    Vector2 _swipe;
    bool _touch;

    const float Chek = 50;

    public enum Direction { Left, Right, Up, Down};
    bool[] swipe = new bool[4];

    public delegate void SwipeDelegate(bool[] swipe);
    public SwipeDelegate SwipeEvent;

    public delegate void ClickDelegate(Vector2 pos);
    public ClickDelegate ClickEvent;

    Vector2 TouchPos()
    {
        return (Vector2)Input.mousePosition;
    }

    bool TouchB()
    {
        return Input.GetMouseButtonDown(0);
    }
    bool TouchE()
    {
        return Input.GetMouseButtonUp(0);
    }
    bool GetTouch()
    {
        return Input.GetMouseButton(0);
    }
    void SendSw()
    {
        if (swipe[0] || swipe[1] || swipe[2] || swipe[3])
        {
            Debug.Log(swipe[0] + "|" + swipe[1] + "|" + swipe[2] + "|" + swipe[3]);
            SwipeEvent?.Invoke(swipe);
        }
        else
        {
            Debug.Log("Click");
            ClickEvent?.Invoke(TouchPos());
        }
        Reset();
    }
    private void Reset()
    {
        _startTouch = _swipe = Vector2.zero;
        _touch = false;

        for (int i = 0; i < 4; i++)
        {
            swipe[i] = false;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        if (TouchB())
        {
            _startTouch = TouchPos();
            _touch = true;
        }
        if (TouchE() && _touch == true) 
        {
            _touch = false;
            SendSw();
        }

        _swipe = Vector2.zero;
        if (_touch && GetTouch())
        {
            _swipe = TouchPos() - _startTouch;
        }

        if (_swipe.magnitude > Chek)
        {
            if (Mathf.Abs(_swipe.x) > Mathf.Abs(_swipe.y))
            {
                swipe[(int)Direction.Left] = _swipe.x < 0;
                swipe[(int)Direction.Right] = _swipe.x > 0;
            }
            else
            {
                swipe[(int)Direction.Up] = _swipe.y < 0;
                swipe[(int)Direction.Down] = _swipe.y > 0;
            }
            SendSw();
        }
    }
}
