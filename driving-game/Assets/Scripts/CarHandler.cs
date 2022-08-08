using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField]
    private float _power;
    [SerializeField]
    private float _gainSpeedPerSecond;
    [SerializeField]
    private float _steeringSpeed;
    private int _steerValue = 0;
    private float _speed;

    // Start is called before the first frame update
    private void Start()
    {
        _speed = _power * Time.deltaTime;
    }

    // Update is called once per frame
    private void Update()
    {
        Acceletare();
        Rotate();
        Forward();
    }

    private void Acceletare()
    {
        _speed += _gainSpeedPerSecond * Time.deltaTime;
    }

    private void Forward()
    {
        transform.Translate(Vector3.forward * _speed);
    }

    private void Rotate()
    {
        transform.Rotate(_steeringSpeed * _steerValue * Time.deltaTime, 0f, 0f);
    }

    public void Steer(int value)
    {
        _steerValue = value;
    }
}
