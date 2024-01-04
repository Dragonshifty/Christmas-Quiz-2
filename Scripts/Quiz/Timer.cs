using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System;

public class Timer : MonoBehaviour
{

    public event Action CountdownReachedZero;
    public event Action DelayTimeReachedZero;
    [SerializeField] int answerTime = 15;
    [SerializeField] int delayTime = 5;
    [SerializeField] TextMeshProUGUI timerShow;
    public bool goTimer = false;
    public bool goDelayTimer = true;
    

    public async void StartTimer()
    {
        goTimer = true;
        goDelayTimer = true;
        await StartCountdown(answerTime);
    }

    public async void InterruptTimer()
    {
        goTimer = false;
        await RunDelayTimer(delayTime);
    }

    async Task StartCountdown(int countdownTime)
    {
        while (countdownTime > 0 && goTimer)
        {
            timerShow.text = countdownTime.ToString();
            await Task.Delay(1000);
            --countdownTime;    
        }

        if (countdownTime == 0) CountdownReachedZero?.Invoke();

        await RunDelayTimer(delayTime);
    }

    async Task RunDelayTimer(int delayTimer)
    {
        while (delayTimer > 0 && goDelayTimer)
        {
            timerShow.text = delayTimer.ToString();
            await Task.Delay(1000);
            --delayTimer;
        }
       if (delayTimer < 1) DelayTimeReachedZero?.Invoke();
        
    }



}
