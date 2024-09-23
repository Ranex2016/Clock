using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class ApiClock : MonoBehaviour
{
    public DateTime syncedTime;

    // Сериализуемый класс json API Yandex
    public class Clocks
    {
        public long time { get; set; }
        //public string clocks { get; set; }
    }

    void Start()
    {
        StartCoroutine(GetRequest("https://yandex.com/time/sync.json"));
    }

    public void OnRefresh()
    {
        Start();
    }

    IEnumerator GetRequest(String uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Ошибка при получении времени: " + webRequest.error);
            }
            else
            {
                // Решить что делать с ответом
                string jsonResult = webRequest.downloadHandler.text;
                Clocks clocks = JsonConvert.DeserializeObject<Clocks>(jsonResult);

                // Преобразуем полученное время из миллисекунд в DateTime
                long milliseconds = clocks.time;
                syncedTime = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).DateTime;
            }
        }
    }

}
