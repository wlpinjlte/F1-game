using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadScene(int index) {
        SceneManager.LoadScene(index);
    }

    public void Exit() {
        Application.Quit();
    }
}
