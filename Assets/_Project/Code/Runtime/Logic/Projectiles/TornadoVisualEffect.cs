using System;
using System.Collections.Generic;
using Code.Runtime.Logic.VisualEffects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.Projectiles
{
    public class TornadoVisualEffect : VisualEffect
    {
        private List<Collider> _collidersInRadius;
        private LayerMask _hittable;
        
        public void Initialize(float lifeTime)
        {
            ReturnToPoolTask(lifeTime).Forget();
        }
        
        private async UniTask ReturnToPoolTask(float lifeTime)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(lifeTime));
            ReturnToPool();
        }

    }
}
