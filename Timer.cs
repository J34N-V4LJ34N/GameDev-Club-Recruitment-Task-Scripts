using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
    public float timeLeft=30;
    public bool timerOn = false;
    public TextMeshProUGUI timerTxt;
    void Start()
    {
        timerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft-=Time.deltaTime;
                updateTimer(timeLeft);
                if (timeLeft < 10)
                {
                    timerTxt.color = Color.red;
                }
                else
                {
                    timerTxt.color = Color.white;
                }
            }
            else
            {
                timeLeft=0;
            }
        }
    }
    void updateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes= Mathf.FloorToInt(currentTime/60);
        float seconds= Mathf.FloorToInt(currentTime%60);
        timerTxt.text=string.Format("{0:00}:{1:00}",minutes,seconds);
    }
}
