using UnityEngine;

namespace GyroSpace.Utility
{
    public class Timer
    {
        private float _startTime, _currentTime;

        public float _CurrentTime { get => _currentTime; }

        public Timer(float newStartTime)
        {
            _startTime = newStartTime;
            _currentTime = _startTime;
        }

        public void Tick()
        {
            _currentTime -= Time.fixedDeltaTime;
        }

        public void Reset()
        {
            _currentTime = _startTime;
        }
    }
}