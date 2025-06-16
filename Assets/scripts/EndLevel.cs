using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private GameManager gameManager;

    void Start() {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        gameManager.setBestTime();
        gameManager.Start();
        // SceneManager.LoadScene(1);
    }
}
