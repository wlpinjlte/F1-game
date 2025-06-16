using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestTimeText;
    public int _captureEveryNFrames;
    public GameObject carPrefab; 
    private GameObject car;
    private float time;
    public int maxTime;
    private ReplaySystem _system;
    private Camera CameraScript;
    private void Awake() {
        _system = new ReplaySystem(this);
        CameraScript = GameObject.FindObjectOfType<Camera>();
    } 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        car = Instantiate(carPrefab, new Vector3(128.52f, 0.2f, 10.93f), Quaternion.Euler(0, -138.19f, 0));
        CameraScript.Start();
        _system.StartRun(car.transform, _captureEveryNFrames);

        GameObject carReplay = Instantiate(carPrefab);

        Renderer[] renderers = carReplay.GetComponentsInChildren<Renderer>(includeInactive: true);
        foreach (Renderer renderer in renderers)
        {
            foreach (Material mat in renderer.materials)
            {
                // Tryb renderowania: Transparent
                mat.SetFloat("_Mode", 3);
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;

                if (mat.HasProperty("_Metallic")) mat.SetFloat("_Metallic", 0f);
                if (mat.HasProperty("_Glossiness")) mat.SetFloat("_Glossiness", 0f); // Standard Shader
                if (mat.HasProperty("_Smoothness")) mat.SetFloat("_Smoothness", 0f); // URP
            }
        }

        // Dezaktywuj fizykę
        Collider[] colliders = carReplay.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.isTrigger = true;
        }

        Rigidbody rb = carReplay.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        _system.PlayRecording(RecordingType.Best, carReplay); 

        time = 0f;

        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (bestTime < float.MaxValue)
        {
            bestTimeText.text = "Best Time: " + bestTime.ToString("F2");
        }
        else
        {
            bestTimeText.text = "Best Time: --";
        }
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        timeText.text = "Time: " + time.ToString("F2");
        if (time >= maxTime) {

        }
    }

    public void setBestTime() {
        _system.FinishRun();
        _system.StopReplay();

        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (time < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", time);
            PlayerPrefs.Save();
            bestTimeText.text = "Best Time: " + time.ToString("F2");
        }

        if (car != null)
        {
            Destroy(car);
        }
    }
}
