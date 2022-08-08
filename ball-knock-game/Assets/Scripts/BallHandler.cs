using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField]
    private float _secondsDetachAfterLaunch;
    [SerializeField]
    private float _secondsRespawnDelay;
    [SerializeField]
    private Rigidbody2D _pivot;
    [SerializeField]
    private GameObject _ballPrefab;

    private Camera _mainCamera;
    private bool _isCurrentBallDragging = false;
    private Rigidbody2D _currentBallRigidBody;
    private SpringJoint2D _currentBallSpringJoint;

    // Start is called before the first frame update
    private void Start()
    {
        SpawnBall();
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
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

    private void LaunchBall()
    {
        _currentBallRigidBody.isKinematic = false;
        Invoke(nameof(DetachBall), _secondsDetachAfterLaunch);
    }

    private void DetachBall()
    {
        _currentBallSpringJoint.enabled = false;
        _currentBallSpringJoint = null;


        Invoke(nameof(SpawnBall), _secondsRespawnDelay);
    }

    private void SpawnBall()
    {
        var newBall = Instantiate(_ballPrefab, _pivot.position, Quaternion.identity);
        _currentBallRigidBody = newBall.GetComponent<Rigidbody2D>();
        _currentBallSpringJoint = newBall.GetComponent<SpringJoint2D>();
        _currentBallSpringJoint.connectedBody = _pivot;
    }
}
