using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Utilities
{
    public class Timer : IDisposable
    {
        private CancellationTokenSource _cancellation;
        
        private readonly TimeSpan _duration;
        private readonly Action _callback;
        
        private TimeSpan _durationLeft;
        private DateTime _startTime;
        
        public Timer(TimeSpan duration, Action callback)
        {
            _duration = duration;
            _callback = callback;
            
            StartTimerFromBeginning().Forget();
        }
        
        public void Cancel()
        {
            _cancellation.Cancel();
        }
        
        private UniTaskVoid StartTimerFromBeginning()
        {
            return StartTimer(_duration);
        }
        
        private async UniTaskVoid StartTimer(TimeSpan duration)
        {
            _cancellation?.Dispose();
            _cancellation = new CancellationTokenSource();
            
            _startTime = DateTime.Now;
            await UniTask.Delay(duration, cancellationToken: _cancellation.Token).SuppressCancellationThrow();
            if (_cancellation == null || _cancellation.IsCancellationRequested)
                return;

            _callback.Invoke();
        }
        
        public void Dispose()
        {
            if (_cancellation == null)
                return;
            
            _cancellation?.Cancel();
            _cancellation?.Dispose();
            _cancellation = null;
        }
    }
}