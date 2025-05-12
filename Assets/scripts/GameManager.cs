using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestTimeText;
    private float time;
    public int maxTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (time < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", time);
            PlayerPrefs.Save();
            bestTimeText.text = "Best Time: " + time.ToString("F2");
        }
    }
}
