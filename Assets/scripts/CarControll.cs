using UnityEngine;

public class CarControll : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    public float maxSteeringAngle = 35f;
    public float motorForce = 1000f;
    public float brakeForce = 3000f;

    void Awake() {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.mass = 1200f;
    }

    private void Start() {
        WheelFrictionCurve sidewaysFriction = frontLeftWheelCollider.sidewaysFriction;
        sidewaysFriction.stiffness = 2f;

        frontLeftWheelCollider.sidewaysFriction = sidewaysFriction;
        frontRightWheelCollider.sidewaysFriction = sidewaysFriction;
        rearLeftWheelCollider.sidewaysFriction = sidewaysFriction;
        rearRightWheelCollider.sidewaysFriction = sidewaysFriction;
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;

        /*brakeForce = isBreaking ? 3000f : 0f;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;*/
    }
}
