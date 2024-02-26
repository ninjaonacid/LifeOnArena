using UnityEngine;

namespace Code.Runtime.Logic.Timer
{
    public class Timer : ITimer
    {
        private float _startTime;
        public float Elapsed => Time.time - _startTime;

        public Timer()
        {
            _startTime = Time.time;
        }
        public void Reset()
        {
            _startTime = Time.time;
        }
    }
}
