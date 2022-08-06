using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _currentBallRigidBody;

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
            _currentBallRigidBody.isKinematic = false;
            return;
        }

        var touchScreenPos = Touchscreen.current.position.ReadValue();
        var touchPos = _mainCamera.ScreenToWorldPoint(touchScreenPos);

        _currentBallRigidBody.isKinematic = true;
        _currentBallRigidBody.position = touchPos;
    }
}
