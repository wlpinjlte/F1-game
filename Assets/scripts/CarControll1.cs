using UnityEngine;

public class CarControll1 : MonoBehaviour
{
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public float motorTorque = 10000f; // Sta³a moc silnika
    public float maxSteeringAngle = 25f; // Maksymalny k¹t skrêtu

    void start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 1000000f;
    }

    void FixedUpdate()
    {
        MoveForward();
        HandleSteering();
    }

    void MoveForward()
    {
        rearLeftWheel.motorTorque = motorTorque;
        rearRightWheel.motorTorque = motorTorque;
    }

    void HandleSteering()
    {
        float steering = Input.GetAxis("Horizontal") * maxSteeringAngle;
        frontLeftWheel.steerAngle = steering;
        frontRightWheel.steerAngle = steering;
    }
}
