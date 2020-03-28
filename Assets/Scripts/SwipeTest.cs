﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeTest : MonoBehaviour
{

    public Swipe swipeControls;
    public Transform player;
    private Vector3 desiredPosition;


    // Update is called once per frame
    void Update()
    {
        if (swipeControls.SwipeLeft)
        {
            desiredPosition += Vector3.left;
        }

        if (swipeControls.SwipeRight)
        {
            desiredPosition += Vector3.right;
        }

        if (swipeControls.SwipeUp)
        {
            desiredPosition += Vector3.forward;
        }

        if (swipeControls.SwipeDown)
        {
            desiredPosition += Vector3.back;
        }

        player.transform.position = Vector3.MoveTowards(player.transform.position, desiredPosition, 3f * Time.deltaTime);

        if (swipeControls.Tap)
        {
            Debug.Log("Tap");
        }
    }
}
