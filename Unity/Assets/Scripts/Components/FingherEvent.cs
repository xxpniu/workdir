﻿using UnityEngine;
using System.Collections;
using System;

public class FingherEvent : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Action OnLeft;

    public Action OnRight;

    public void OnSwipe(SwipeGesture gesuter)
    {
        switch (gesuter.Direction)
        {
            case FingerGestures.SwipeDirection.Left:
		    case  FingerGestures.SwipeDirection.LowerLeftDiagonal:
		    //case FingerGestures.SwipeDirection.UpperLeftDiagonal:
                if (OnLeft != null) OnLeft();
                break;
		    case  FingerGestures.SwipeDirection.UpperRightDiagonal:
            case FingerGestures.SwipeDirection.Right:
                if (OnRight != null) OnRight();
                break;
        }
    }
}
