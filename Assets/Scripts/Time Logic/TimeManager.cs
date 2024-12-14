using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private const int minutesInHour = 60;
    private const int degreesInClockPerMinute = 30;

    [SerializeField] private FloatSO currentTime;

    public bool timePassing;
    public float minutesSpent;
    private float hours;
    private float minutes;

    //real clock sprite control
    [SerializeField] private RectTransform minuteHand;
    [SerializeField] private RectTransform hourHand;




    private void Start()
    {
        timePassing = false;
        UpdateClock(0);
    }

    public void UpdateClock(float addedMinutes)
    {
        minutesSpent = addedMinutes / 60;
        Debug.Log("Minutes spent: " + minutesSpent);
        currentTime.Value += minutesSpent;
        Debug.Log("Current minutes: " + currentTime.Value);
        hourHand.rotation = Quaternion.Euler(0, 0, -GetHour());
        minuteHand.rotation = Quaternion.Euler(0, 0, -GetMinutes());
    }


    public float GetHour()
    {
        hours = currentTime.Value * degreesInClockPerMinute;
        return hours;
    }


    public float GetMinutes()
    {
        minutes = currentTime.Value % 1 * minutesInHour * 6;
        return minutes;
    }
}
