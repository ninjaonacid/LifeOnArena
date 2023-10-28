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
        private readonly Dictionary<int, Stack<GameObject>> _enemyObjectsStock;

        private readonly IEnemyFactory _enemyFactory;
        public EnemyObjectPool(IEnemyFactory enemyFactory)
        {
            _enemyObjectsStock = new Dictionary<int, Stack<GameObject>>();
            _enemyFactory = enemyFactory;
        }

        public void Cleanup()
        {
            _enemyObjectsStock.Clear();
        }

        public async UniTask<GameObject> GetObject(int mobId, Transform parent,
                                                    CancellationToken token)
        {
            GameObject result = null;

            if (CheckForExist(mobId))
            {
                result = _enemyObjectsStock[mobId].Pop();

            }
            else
            {
                if (!_enemyObjectsStock.ContainsKey(mobId))
                    _enemyObjectsStock.Add(mobId, new Stack<GameObject>());

                result = await _enemyFactory.CreateMonster(mobId, parent, token);
            }

            result.transform.position = parent.position;
            result.SetActive(true);
            return result;
        }

        public void ReturnObject(int mobId, GameObject obj)
        {
            _enemyObjectsStock[mobId].Push(obj);

        }

        private bool CheckForExist(int mobId)
        {
            return _enemyObjectsStock.ContainsKey(mobId) && _enemyObjectsStock[mobId].Count > 0;
        }

        
    }
}
