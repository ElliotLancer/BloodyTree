using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private WheelCollider _wheel1;
    [SerializeField] private WheelCollider _wheel2;
    [SerializeField] private WheelCollider _wheel3;
    [SerializeField] private WheelCollider _wheel4;

    [SerializeField] private float _driveSpeed = 1800f;
    [SerializeField] private float _reverseSpeed = 900f;
    [SerializeField] private float _steerSpeed = 25f;
    [SerializeField] private float _acceleration = 1200f;
    [SerializeField] private float _brakeForce = 3000f;

    [SerializeField] private float _minSteerAngle = 10f;
    [SerializeField] private float _speedForReducedSteering = 20f;

    private bool _breaking;
    private float _horizontalInput;
    private float _verticalInput;
    private bool _canDrive;

    private float _currentMotor;
    private float _currentSteer;

    private void Update()
    {
        if (!_canDrive)
            return;

        _breaking = Input.GetKey(KeyCode.Space);

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (!_canDrive)
        {
            ApplyMotor(0f);
            ApplyBrakes(0f);
            return;
        }

        float speed = _rb.linearVelocity.magnitude;
        float forwardSpeed = transform.InverseTransformDirection(_rb.linearVelocity).z;

        float speedFactor = Mathf.Clamp01(
            1f - speed / _speedForReducedSteering
        );

        float steerAngle = Mathf.Lerp(
            _minSteerAngle,
            _steerSpeed,
            speedFactor
        );

        float targetSteer = _horizontalInput * steerAngle;

        _currentSteer = Mathf.MoveTowards(
            _currentSteer,
            targetSteer,
            60f * Time.fixedDeltaTime
        );

        _wheel1.steerAngle = _currentSteer;
        _wheel2.steerAngle = _currentSteer;

        float targetMotor = 0f;
        float brake = 0f;

        if (_breaking)
        {
            brake = _brakeForce;
        }
        else if (_verticalInput > 0f)
        {
            targetMotor = _verticalInput * _driveSpeed;
        }
        else if (_verticalInput < 0f)
        {
            if (forwardSpeed > 0.5f)
            {
                brake = _brakeForce;
            }
            else
            {
                targetMotor = _verticalInput * _reverseSpeed;
            }
        }

        _currentMotor = Mathf.MoveTowards(
            _currentMotor,
            targetMotor,
            _acceleration * Time.fixedDeltaTime
        );

        ApplyMotor(_currentMotor);
        ApplyBrakes(brake);
    }

    private void ApplyMotor(float torque)
    {
        _wheel3.motorTorque = torque;
        _wheel4.motorTorque = torque;
    }

    private void ApplyBrakes(float torque)
    {
        _wheel1.brakeTorque = torque;
        _wheel2.brakeTorque = torque;
        _wheel3.brakeTorque = torque;
        _wheel4.brakeTorque = torque;
    }

    public void SetCanDrive(bool value)
    {
        _canDrive = value;

        if (!value)
        {
            _currentMotor = 0f;
            _currentSteer = 0f;

            ApplyMotor(0f);
            ApplyBrakes(0f);

            _wheel1.steerAngle = 0f;
            _wheel2.steerAngle = 0f;
        }
    }
}
