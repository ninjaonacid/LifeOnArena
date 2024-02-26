using System;
using System.Threading;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.VisualEffects
{
    public class VisualEffect : PooledObject
    {
        [SerializeField] private ParticleSystem _particleSystem;
        public event Action<VisualEffect> Finished;
        private CancellationTokenSource _cts;

        public void Play()
        {
            _particleSystem.Play();
            
            if (_particleSystem.main.loop)
            {
                WaitForDurationEnd(TaskHelper.CreateToken(ref _cts)).Forget();
            }
        }

        public void Stop()
        {
            _particleSystem.Stop();
            if (_cts.IsCancellationRequested)
            {
                _cts.Token.ThrowIfCancellationRequested();
            }
        }
        private async UniTask WaitForDurationEnd(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_particleSystem.main.duration), cancellationToken: token);
            _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            await UniTask.WaitUntil(() => _particleSystem.particleCount == 0, cancellationToken: token);
            Finished?.Invoke(this);
        }
    }
}
