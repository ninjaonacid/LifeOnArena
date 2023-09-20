using System.Threading;
using Code.ConfigData.Identifiers;
using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Core.ObjectPool
{
    public interface IEnemyObjectPool :  IService
    {
        public UniTask<GameObject> GetObject(MobId mobId, Transform parent, CancellationToken token);
        public void ReturnObject(MobId mobId, GameObject obj);

        void Cleanup();
    }
}