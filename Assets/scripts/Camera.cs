using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform car;
    public Vector3 offset = new Vector3(0, 5, -15); 
    public float smoothSpeed = 5f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Oblicz docelow¹ pozycjê kamery
        Vector3 targetPosition = car.position + car.TransformDirection(offset);

        // P³ynne przesuwanie kamery
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Kamera zawsze patrzy na bolid
        transform.LookAt(car);
    }
}
