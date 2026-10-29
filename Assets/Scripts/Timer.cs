using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public float temps = 0f;
    bool enCours = true;

    void Update()
    {
        if (!enCours) return;

        temps += Time.deltaTime;
        timerText.text = FormatTemps(temps);
    }

    string FormatTemps(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60);
        int secondes = Mathf.FloorToInt(t % 60);
        return minutes.ToString("00") + ":" + secondes.ToString("00");
    }
}

