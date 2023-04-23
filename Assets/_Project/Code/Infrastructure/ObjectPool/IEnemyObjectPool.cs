using System.Threading;
using Code.Services;
using Code.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.ObjectPool
{
    public interface IEnemyObjectPool :  IService
    {
        public UniTask<GameObject> GetObject(MonsterTypeId monsterTypeId, Transform parent, CancellationToken token);
        public void ReturnObject(MonsterTypeId monsterTypeId, GameObject obj);

        void Cleanup();
    }
}