using System.Collections.Generic;
using System.Threading;
using Code.ConfigData.Identifiers;
using Code.Core.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Core.ObjectPool
{
    public class EnemyObjectPool
    {

        public Dictionary<MobId, List<GameObject>> _enemyObjectsStock;

        private readonly IEnemyFactory _enemyFactory;
        public EnemyObjectPool(IEnemyFactory enemyFactory)
        {
            _enemyObjectsStock = new Dictionary<MobId, List<GameObject>>();
            _enemyFactory = enemyFactory;
        }

        public void Cleanup()
        {
            _enemyObjectsStock.Clear();
        }

        public async UniTask<GameObject> GetObject(MobId mobId, Transform parent,
                                                    CancellationToken token)
        {
            GameObject result = null;

            if (CheckForExist(mobId))
            {
                result = _enemyObjectsStock[mobId][0];
                _enemyObjectsStock[mobId].RemoveAt(0);
            }
            else
            {
                if (!_enemyObjectsStock.ContainsKey(mobId))
                    _enemyObjectsStock.Add(mobId, new List<GameObject>());

                result = await _enemyFactory.CreateMonster(mobId, parent, token);
            }

            result.transform.position = parent.position;
            result.SetActive(true);
            return result;
        }

        public void ReturnObject(MobId mobId, GameObject obj)
        {
            _enemyObjectsStock[mobId].Add(obj);

        }

        private bool CheckForExist(MobId mobId)
        {
            return _enemyObjectsStock.ContainsKey(mobId) && _enemyObjectsStock[mobId].Count > 0;
        }

        
    }
}
