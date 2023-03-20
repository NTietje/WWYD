using System;
using UnityEngine;
using UnityEngine.UI;

public class ClockCounter : MonoBehaviour
{
    [SerializeField] private Text clockText;
    [SerializeField] private int timeHours = 0;
    [SerializeField] private int timeMinutes = 0;
    [SerializeField] private int timeSeconds = 0;
    private float time = 0;

    private void Start()
    {
        String text = string.Format("{0:00}:{1:00}:{2:00}", timeHours, timeMinutes, timeSeconds);
        clockText.text = text;

        time = timeHours * 60 * 60;
        time += timeMinutes * 60;
        time += timeSeconds;
    }

    void Update()
    {
        time -= Time.deltaTime;

        int seconds = Mathf.FloorToInt(time % 60f);
        int restMinutes = Mathf.FloorToInt(time / 60f);
        int hours = Mathf.FloorToInt(restMinutes / 60f);
        int minutes = Mathf.FloorToInt(restMinutes - hours * 60f);

        //Debug.Log(string.Format("{0:00}:{1:00}", minutes, seconds));

        String text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        clockText.text = text;
        
        if (time <= 0)
        {
            Debug.Log("Time's up!");
        }
    }
}
