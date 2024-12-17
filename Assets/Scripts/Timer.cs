using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro; // https://www.youtube.com/watch?v=u_n3NEi223E
using UnityEditor.Timeline;
using UnityEngine;

public class Timer : MonoBehaviour {
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    /* [Header("Format Settings")]
     * public bool hasFormat;
     * public TimerFormats format;
     * private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>(); */

    // Start is called before the first frame update
    void Start() {
        /* timeFormats.Add(TimerFormats.Whole, "0");
         * timeFormats.Add(TimerFormats.Tenths, "0.0");
         * timeFormats.Add(TimerFormats.Hundredths, "0.00"); */
    }

    // Update is called once per frame
    void Update() {
        // "Time will either decrement every second under '-=', or increment under '+='."
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
        
        // "Are you below or above the limit? (read from left to right)"
        if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit))) {
            // "This also acts as a stopwatch."
            currentTime = timerLimit;
            timerText.color = Color.red;
            enabled = false;
        }
        
        SetTimerText();
    }

    private void SetTimerText() {
        // "Alternative formats from the video listed above."
        /* timerText.text = currentTime.ToString("0.00");
         * timerText.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString(); */

        int minutes = Mathf.FloorToInt(currentTime % 3600 / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        int centiseconds = Mathf.FloorToInt(currentTime % 1 * 100);
        timerText.text = $"{minutes:00}:{seconds:00}:{centiseconds:00}";
        
    }
}

/* public enum TimerFormats {
 *   Whole, Tenths, Hundredths
 * } */
