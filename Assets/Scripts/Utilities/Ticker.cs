using System;

namespace Utilities
{
    public class Ticker : IDisposable
    {
        private Timer _timer;
        private bool _isDisposed;
        
        private readonly TimeSpan _durationBetweenTicks;
        private readonly Action _onTick;

        public Ticker(TimeSpan durationBetweenTicks, Action onTick)
        {
            _durationBetweenTicks = durationBetweenTicks;
            _onTick = onTick;

            DispatchTimer();
        }

        private void DispatchTimer()
        {
            _timer = new Timer(_durationBetweenTicks, OnTick);
        }

        private void OnTick()
        {
            if (_isDisposed)
                return;
            
            _onTick?.Invoke();

            RenewTimer();
        }

        private void RenewTimer()
        {
            DisposeTimer();
            DispatchTimer();
        }

        private void DisposeTimer()
        {
            _timer?.Dispose();
            _timer = null;
        }

        public void Dispose()
        {
            _isDisposed = true;
            
            if (_timer != null)
                DisposeTimer();
        }
    }
}