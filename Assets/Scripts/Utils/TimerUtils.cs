
using System;
using UnityEngine;

namespace TimerUtils
{
    /// <summary>
    /// Timer class for Unity, can be used as a timer or as a countdown timer.
    /// How to use:
    /// <example>
    /// 1) In OnEnable method create a new timer.  
    /// 2) Subscribe to the OnTimerFinished event with the method you want to trigger.
    /// 3) On Update call timer.ExecuteTimer();
    /// 4) Remember to call timer.ResetTimer() when you want to restart the timer.
    /// 5) Remember to unsubscribe from the event when you don't need it, in OnDisable.
    /// </example>
    /// </summary>
    public class Timer
    {
        private float timerMaxValue;
        public float TimerMaxValue 
        {
            get => timerMaxValue;
            set => timerMaxValue = (value > 0) ? value : timerMaxValue;
        }
        private float timerCurrentValue;

        public event Action OnTimerFinished;


        public Timer(float timerMaxValue)
        {
            TimerMaxValue = timerMaxValue;
            timerCurrentValue = TimerMaxValue;
        }

        public void ExecuteTimer()
        {
            timerCurrentValue -= Time.deltaTime;
            if (timerCurrentValue <= 0)
            {
                OnTimerFinished?.Invoke();
                ResetTimer();
            }
        }

        public void ResetTimer()
        {
            timerCurrentValue = TimerMaxValue;
        }
    }

}

