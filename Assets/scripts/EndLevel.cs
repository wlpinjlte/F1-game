using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private GameManager gameManager;

    void Start() {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<CarAgent>() != null)
        {
            var carAgent = other.GetComponent<CarAgent>();
            carAgent.AddReward(0.5f);
            carAgent.finished = true;
            Debug.Log("meta");
        }
        gameManager.setBestTime();
        // SceneManager.LoadScene(1);
    }
}
