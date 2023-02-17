using UnityEngine;

namespace Code.Logic
{
    public class Timer : ITimer
    {
        public float startTime;
        public float Elapsed => Time.time - startTime;

        public Timer()
        {
            startTime = Time.time;
        }
        public void Reset()
        {
            startTime = Time.time;
        }
    }
}
