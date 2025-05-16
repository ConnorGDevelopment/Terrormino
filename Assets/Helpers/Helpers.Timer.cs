using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Helpers
{
    public class Timer

    {
        public float Time => _time;
        private float _time = 0;

        public struct SAlarm
        {
            public float BaseTime;
            public (float, float) Variance;
            public float Value;

            public SAlarm(float baseTime, float minVariance = 0, float maxVariance = 0) : this()
            {
                BaseTime = baseTime;
                Variance = (minVariance, maxVariance);
            }

            public void Generate()
            {
                Value = BaseTime + Random.Range(Variance.Item1, Variance.Item2);
            }
        }
        public SAlarm Alarm = new();

        public bool Paused = false;


        public enum TimerState
        {
            Paused,
            Running,
            Ringing,
        }
        public TimerState CurrentState
        {
            get
            {
                if (Paused)
                {
                    return TimerState.Paused;
                }
                else if (Time >= Alarm.Value)
                {
                    return TimerState.Ringing;
                }
                else
                {
                    return TimerState.Running;
                }
            }
        }
        public void Tick(float deltaTime)
        {
            if (CurrentState == TimerState.Running)
            {
                _time += deltaTime;
            }
        }
        public void ResetTimer()
        {
            _time = 0;
            Alarm.Generate();
        }

        public static List<Timer> FilterByState(List<Timer> timers, TimerState timerState)
        {
            return timers.Where(timer => timer.CurrentState == timerState).ToList();
        }
    }
}