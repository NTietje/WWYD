using UnityEngine;

public class timercounter : MonoBehaviour
{
    float currentTime = 50f;

    void Update()
    {
        currentTime -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        Debug.Log(string.Format("{0:00}:{1:00}", minutes, seconds));

        if (currentTime <= 0)
        {
            Debug.Log("Time's up!");
        }
    }
}
