using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    private Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            return;
        }
        var touchPos = Touchscreen.current.position.ReadValue();
        Debug.Log(touchPos);
        Debug.Log(_mainCamera.ScreenToWorldPoint(touchPos));
    }
}
