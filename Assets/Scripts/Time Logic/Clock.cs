using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private TimeManager tm;
    private const float hoursToDegrees = 360 / 12;
    private const float minutesToDegrees = 360 / 60;
    //debug clock text control
    [SerializeField] private TextMeshProUGUI display;

    //real clock sprite control
    [SerializeField] private GameObject minuteHand;
    [SerializeField] private GameObject hourHand;


    // Update is called once per frame
    private void Update()
    {
        display.text = tm.Clock12Hour();
        hourHand.transform.rotation = Quaternion.Euler(0,0, -tm.GetHour() * hoursToDegrees);
        minuteHand.transform.rotation = Quaternion.Euler(0, 0, -tm.GetMinutes() * minutesToDegrees);
    }
}
