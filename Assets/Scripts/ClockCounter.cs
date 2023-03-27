using System;
using UnityEngine;
using UnityEngine.UI;

public class ClockCounter : MonoBehaviour
{
    [SerializeField] private Text clockText;
    [Header("Will use time of GameStoryManager if no time is set here")]
    [SerializeField] private int timeHours = 0;
    [SerializeField] private int timeMinutes = 0;
    [SerializeField] private int timeSeconds = 0;
    private float time = 0;
    private bool countingAllowed = true;

    public static int[] TimeToIntArray(float time)
    {
        int seconds = Mathf.FloorToInt(time % 60f);
        int restMinutes = Mathf.FloorToInt(time / 60f);
        int hours = Mathf.FloorToInt(restMinutes / 60f);
        int minutes = Mathf.FloorToInt(restMinutes - hours * 60f);
        return new[] { hours, minutes, seconds };
    }

    public static float IntsToTime(int[] timeArray)
    {
        float time = 0;
        time = timeArray[0] * 60 * 60;
        time += timeArray[1] * 60;
        time += timeArray[2];
        return time;
    }

    private void Start()
    {
        Debug.Log("is in Start of ClockCount");
        if (timeHours == 0 & timeMinutes == 0 & timeSeconds == 0)
        {
            // if 0, 0, 0 as input use the time values in the GameStoryManager
            var timeTillBang = GameStoryManager.Instance.GetTimeTillBang();
            timeHours = timeTillBang[0];
            timeMinutes = timeTillBang[1];
            timeSeconds = timeTillBang[2];
            
        }
        String text = string.Format("{0:00}:{1:00}:{2:00}", timeHours, timeMinutes, timeSeconds);
        
        clockText.text = text;
        time = IntsToTime(new[] {timeHours, timeMinutes, timeSeconds});

        if (time <= 0)
        {
            Debug.Log("set clock to small seconds value");
            timeHours = 0;
            timeMinutes = 0;
            timeSeconds = 8;
            GameStoryManager.Instance.SetClockTime(timeHours, timeMinutes, timeSeconds);
            text = string.Format("{0:00}:{1:00}:{2:00}", timeHours, timeMinutes, timeSeconds);
            clockText.text = text;
        }
    }

    void Update()
    {
        if (countingAllowed)
        {
            time -= Time.deltaTime;

            int seconds = Mathf.FloorToInt(time % 60f);
            int restMinutes = Mathf.FloorToInt(time / 60f);
            int hours = Mathf.FloorToInt(restMinutes / 60f);
            int minutes = Mathf.FloorToInt(restMinutes - hours * 60f);

            if (time < 0)
            {
                countingAllowed = false;
                Debug.Log("Time's up!");
                if (GameStoryManager.Instance.allowReset)
                {
                    GameStoryManager.Instance.HardResetToStartLevel();
                }
            }
            else
            {
                GameStoryManager.Instance.SetClockTime(hours, minutes, seconds);
                String text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
                clockText.text = text;
            }
        }
    }
}
