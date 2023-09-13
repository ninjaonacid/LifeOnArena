using System.Threading;
using Code.Services;
using Code.StaticData;
using Code.StaticData.Identifiers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.ObjectPool
{
    public interface IEnemyObjectPool :  IService
    {
        public UniTask<GameObject> GetObject(MobId mobId, Transform parent, CancellationToken token);
        public void ReturnObject(MobId mobId, GameObject obj);

        void Cleanup();
    }
}