using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalogClock : MonoBehaviour
{
    DateTime time;
    TimeSpan timeSpan;

    public RectTransform secondHand;
    public RectTransform minuteHand;
    public RectTransform hourHand;

    private const float hoursToDegrees = -360 / 12f;
    private const float minutesToDegrees = -360 / 60f;
    private const float secondsToDegrees = -360 / 60f;

    void Update()
    {
        AnalogTime();
    }

    public void AnalogTime()
    {
        timeSpan = DateTime.Now.TimeOfDay;
        hourHand.rotation = Quaternion.Euler(0, 0, (float)timeSpan.TotalHours * hoursToDegrees);
        minuteHand.rotation = Quaternion.Euler(0, 0, (float)timeSpan.TotalMinutes * minutesToDegrees);
        secondHand.rotation = Quaternion.Euler(0, 0, (float)timeSpan.TotalSeconds * secondsToDegrees);
    }
}
