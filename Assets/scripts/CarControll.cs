using UnityEngine;

public class CarControll1 : MonoBehaviour
{
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public float motorTorque = 10000f; // Sta�a moc silnika
    public float maxSteeringAngle = 25f; // Maksymalny k�t skr�tu

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 1000000f;
        rb.linearDamping = 0.1f; // lub nawet mniejsze
        rb.angularDamping = 0.05f;
    }

    void FixedUpdate()
    {
        MoveForward();
        HandleSteering();
    }

    void MoveForward()
    {
        float Vdirection = Input.GetAxis("Vertical");
        frontLeftWheel.motorTorque = motorTorque * Vdirection;
        frontRightWheel.motorTorque = motorTorque * Vdirection;
    }

    void HandleSteering()
    {
        float steering = Input.GetAxis("Horizontal") * maxSteeringAngle;
        frontLeftWheel.steerAngle = steering;
        frontRightWheel.steerAngle = steering;
    }
}
