using System;
using System.Collections.Generic;
using System.Linq;
using Modules.TimeUtilities.Timers;

namespace Modules.Scheduler
{
    public sealed class ScheduledAction : IDisposable
    {
        private readonly List<CountdownTimer> _timersPool = new();
        private readonly Dictionary<CountdownTimer, Action> _internalCallbacks = new();
        
        public void Dispose()
        {
            _timersPool.ForEach(timer => timer.Dispose());
        }

        public void Schedule(Action action, float time)
        {
            CountdownTimer timer = GetTimer();
            timer.Finished += CreateCallback(timer, action);
            timer.Start(time);
        }

        private CountdownTimer GetTimer()
        {
            CountdownTimer timer = _timersPool
                .FirstOrDefault(x => _internalCallbacks.ContainsKey(x) == false);

            if (timer == null)
            {
                timer = new CountdownTimer();
                _timersPool.Add(timer);
            }

            return timer;
        }

        private Action CreateCallback(CountdownTimer timer, Action callback)
        {
            Action internalCallback = () =>
            {
                Action internalCallback = _internalCallbacks[timer];
                timer.Finished -= internalCallback;
                _internalCallbacks.Remove(timer);
                
                callback?.Invoke();
            };
            
            _internalCallbacks.Add(timer, internalCallback);
            
            return internalCallback;
        }
    }
}