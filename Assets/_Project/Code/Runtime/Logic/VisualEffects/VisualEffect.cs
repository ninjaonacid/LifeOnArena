using System;
using System.Linq;
using System.Threading;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.VisualEffects
{
    public class VisualEffect : PooledObject
    {
        [SerializeField] protected ParticleSystem _particleSystem;
        private ParticleSystem[] _childs;
        public event Action<VisualEffect> Finished;
        private CancellationTokenSource _cts;
        
        private void Awake()
        {
            _childs = GetComponentsInChildren<ParticleSystem>();
        }

        public void Play()
        {
            _particleSystem.Play();
            
            if (!_particleSystem.main.loop)
            {
                WaitForDurationEnd(_particleSystem.main.duration, TaskHelper.CreateToken(ref _cts)).Forget();
            }
        }

        public void Play(float duration)
        {
            _particleSystem.Play();
            
            WaitForDurationEnd(duration, TaskHelper.CreateToken(ref _cts)).Forget();
        }
        
        public bool IsPlaying() => _particleSystem.particleCount != 0;

        public void Stop()
        {
            _particleSystem.Stop();
            
            if (_cts.IsCancellationRequested)
            {
                _cts.Token.ThrowIfCancellationRequested();
            }
            
            Finished?.Invoke(this);
        }
        
     
        
        private async UniTask WaitForDurationEnd(float duration, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: token);
            _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            await UniTask.WaitUntil(() =>
            {
                return _childs.All(ps => ps.particleCount == 0);
            }, cancellationToken: token);
            
            Finished?.Invoke(this);
            ReturnToPool();
        }
    }
}
