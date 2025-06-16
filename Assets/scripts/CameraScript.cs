using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform car;
    public Vector3 offset = new Vector3(0, 5, -15); 
    public float smoothSpeed = 5f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        FindCar();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (car == null) {
            FindCar();
            return;
        }
        // Kamera zawsze patrzy na bolid
        transform.LookAt(car);

        // Oblicz docelow� pozycj� kamery
        Vector3 targetPosition = car.position + car.TransformDirection(offset);

        // P�ynne przesuwanie kamery
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
    
    private void FindCar() {
        GameObject foundCar = GameObject.FindGameObjectWithTag("Player");
        if (foundCar != null)
        {
            car = foundCar.transform;
        } else {
            car = null;
        }
    }
}
