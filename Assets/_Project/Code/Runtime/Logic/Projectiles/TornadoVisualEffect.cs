using System;
using System.Collections.Generic;
using Code.Runtime.Logic.VisualEffects;
using Code.Runtime.Services.BattleService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.Projectiles
{
    public class TornadoVisualEffect : VisualEffect
    {
        private BattleService _battleService;
        

        private List<Collider> _collidersInRadius;
        private LayerMask _hittable;
        
        public void Initialize(BattleService battleService, float lifeTime)
        {
            _battleService = battleService;
            ReturnToPoolTask(lifeTime).Forget();
        }
        
        private async UniTask ReturnToPoolTask(float lifeTime)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(lifeTime));
            ReturnToPool();
        }

    }
}
