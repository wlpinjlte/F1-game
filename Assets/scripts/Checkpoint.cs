using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarAgent>() != null)
        {
            other.GetComponent<CarAgent>().AddReward(0.1f);
            // Debug.Log("checkpoint");
        }
    }
}
