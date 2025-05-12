using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float timerElapsed;
    [SerializeField, Tooltip("Tiempo")] private float timerTime;
    private int minutes, seconds, cents;
    // Update is called once per frame
    void Update()
    {
        timerTime -= Time.deltaTime;
        if(timerTime<0) timerTime = 0;

        minutes = (int)(timerTime / 60f);
        seconds = (int)(timerTime - minutes * 60f);
        cents = (int)((timerTime - (int)timerTime) * 100f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
