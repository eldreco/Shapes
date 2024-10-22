using System;
using UnityEngine;

namespace Utils {

    /// <summary>
    ///     Timer class for Unity, can be used as a timer or as a countdown timer.
    /// </summary>
    /// <remarks>
    ///     How to use:
    ///     <list type="number">
    ///         <item>
    ///             <description>In OnEnable method create a new timer using the constructor.</description>
    ///         </item>
    ///         <item>
    ///             <description>Subscribe to the OnTimerFinished event with the method you want to trigger.</description>
    ///         </item>
    ///         <item>
    ///             <description>On Update call ExecuteTimer();</description>
    ///         </item>
    ///         <item>
    ///             <description>Remember to call ResetTimer() when you want to restart the timer.</description>
    ///         </item>
    ///         <item>
    ///             <description>Remember to unsubscribe from the event when you don't need it, in OnDisable.</description>
    ///         </item>
    ///     </list>
    /// </remarks>
    public class Timer {

        private float _timerCurrentValue;
        private float _timerMaxValue;

        public Timer(float timerMaxValue) {
            TimerMaxValue = timerMaxValue;
            _timerCurrentValue = TimerMaxValue;
        }

        public float TimerMaxValue
        {
            get => _timerMaxValue;
            set => _timerMaxValue = value > 0 ? value : _timerMaxValue;
        }

        public event Action OnTimerFinished;

        public void ExecuteTimer() {
            _timerCurrentValue -= Time.deltaTime;

            if (_timerCurrentValue <= 0) {
                OnTimerFinished?.Invoke();
                ResetTimer();
            }
        }

        public void ResetTimer() {
            _timerCurrentValue = TimerMaxValue;
        }
    }
}