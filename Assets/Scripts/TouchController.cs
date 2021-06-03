using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    private Touch lastTouch;
    private Touch currentTouch;

    void Update()
    {
        if (fingerStillDown()) {
            lastTouch = currentTouch;
            currentTouch = getTouch();
        }
        else if (fingerDown()) {
            currentTouch = getTouch();
        }
    }

    //If finger is on screen for current frame, return true
    public bool fingerDown() {
        return Input.touchCount > 0;
    }

    //If finger is on screen for last frame as well as current frame, return true
    public bool fingerStillDown() {
        return fingerDown() && ((getTouch().phase == TouchPhase.Moved) || (getTouch().phase == TouchPhase.Ended));
    }

    public Touch getTouch() {
        return Input.GetTouch(0);
    }

    //Only call if checked for fingerStillDown() first
    public Vector2 getFingerMovement() {
        Vector2 movement =  getFingerPos() - getLastFingerPos();
        return movement;
    }

    //Only call if checked for fingerDown() first
    public Vector2 getFingerPos() {
        Vector2 fingerPos = Camera.main.ScreenToWorldPoint(currentTouch.position);
        return fingerPos;
    }

    public Vector2 getLastFingerPos()  {
        Vector2 fingerPos = Camera.main.ScreenToWorldPoint(lastTouch.position);
        return fingerPos;
    }

    public Touch getLastTouch() {
        return lastTouch;
    }
}
