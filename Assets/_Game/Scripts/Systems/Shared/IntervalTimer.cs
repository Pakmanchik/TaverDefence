using System;
using System.Threading;
using System.Threading.Tasks;

namespace TowerDefence.Systems.Shared
{
    public sealed class IntervalTimer
    {
        public event Action OnTick;
        private CancellationTokenSource _cts;

        public void Start(float duration, float step)
        {
            _cts = new CancellationTokenSource();
            StartTimer(duration, step, _cts.Token);
        }

        private async void StartTimer(float duration, float step, CancellationToken cts)
        {
            while (duration != 0)
            {
                if (cts.IsCancellationRequested)
                    break;

                OnTick?.Invoke();
                duration--;

                await Task.Delay(TimeSpan.FromSeconds(step), cts);
            }

            Cancel();
        }

        public void Cancel() => _cts?.Cancel();
    }
}