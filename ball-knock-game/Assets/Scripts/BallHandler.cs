using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _currentBallRigidBody;
    [SerializeField]
    private float _secondsDetachAfterLaunch;
    [SerializeField]
    private SpringJoint2D _currentBallSpringJoint;

    private Camera _mainCamera;
    private bool _isCurrentBallDragging = false;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentBallSpringJoint == null)
        {
            return;
        }

        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (_isCurrentBallDragging)
            {
                LaunchBall();
            }

            _isCurrentBallDragging = false;
            return;
        }

        _isCurrentBallDragging = true;

        var touchScreenPos = Touchscreen.current.position.ReadValue();
        var touchPos = _mainCamera.ScreenToWorldPoint(touchScreenPos);

        _currentBallRigidBody.isKinematic = true;
        _currentBallRigidBody.position = touchPos;
    }

    void LaunchBall()
    {
        _currentBallRigidBody.isKinematic = false;
        Invoke(nameof(DetachBall), _secondsDetachAfterLaunch);
    }

    void DetachBall()
    {
        _currentBallSpringJoint.enabled = false;
        _currentBallSpringJoint = null;
    }
}
