using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DigitalClock : MonoBehaviour
{
    private TimeSpan currentTime;
    private ApiClock apiClock;
    [SerializeField] private Text display;
    [SerializeField] private float timerSecondSync = 60f; // Таймер в секундах

    private void Start()
    {
        currentTime = new TimeSpan(0, 15, 20);
        apiClock = GetComponent<ApiClock>();
        //запуск таймера синхронизации
        StartCoroutine(SyncTimerYandex());
    }
    void Update()
    {
        DigitalTime();
    }

    public void DigitalTime()
    {
        currentTime += TimeSpan.FromSeconds(Time.deltaTime);
        display.text = currentTime.ToString(@"hh\:mm\:ss");
    }

    public void OnRefresh()
    {
        apiClock.OnRefresh();
        currentTime = new TimeSpan(apiClock.syncedTime.Hour + 3,
                                    apiClock.syncedTime.Minute,
                                    apiClock.syncedTime.Second);
    }

    IEnumerator SyncTimerYandex()
    {
        while (true)
        {
            yield return new WaitForSeconds(timerSecondSync);
            OnRefresh();
        }
    }

}
