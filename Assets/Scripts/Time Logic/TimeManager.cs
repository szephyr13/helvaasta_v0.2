using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private const int hoursInDay = 24;
    private const int minutesInHour = 60;

    [SerializeField] private float dayDuration = 30f;

    [SerializeField] private float totalTime = 0;
    [SerializeField] private float currentTime = 0;

    public bool timePassing;
    public float minutesSpent;

    private void Start()
    {
        timePassing = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            minutesSpent = Random.Range(0.05f, 0.25f);
            timePassing = true;
        }

        if (timePassing)
        {
            StartCoroutine(UpdateClock(minutesSpent));
            
        }
        currentTime = totalTime % dayDuration;
    }

    private IEnumerator UpdateClock(float minutes)
    {
        totalTime += Time.deltaTime;
        yield return new WaitForSecondsRealtime(minutes);
        timePassing = false;
    }

    public float GetHour()
    {
        return currentTime * hoursInDay / dayDuration;
    }

    public float GetMinutes()
    {
        return (currentTime * hoursInDay * minutesInHour / dayDuration) % minutesInHour;
    }

    public string Clock12Hour()
    {
        int hour = Mathf.FloorToInt(GetHour());
        string abbr = "AM";

        if(hour >= 12)
        {
            abbr = "PM";
            hour -= 12;
        }

        if (hour == 0)
        {
            hour = 12;
        }

        return hour.ToString("00") + ":" + Mathf.FloorToInt(GetMinutes()).ToString("00") + " " + abbr;
    }
}
