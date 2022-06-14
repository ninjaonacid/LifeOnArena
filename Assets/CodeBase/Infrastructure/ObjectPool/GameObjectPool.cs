using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.ObjectPool
{
    public class GameObjectPool : IObjectPool
    {
        public List<GameObject> _enemyObjectsStock;
        private readonly IGameFactory _gameFactory;
        public GameObjectPool(IGameFactory gameFactory)
        {
            _enemyObjectsStock = new List<GameObject>();
            _gameFactory = gameFactory;
        }

        public GameObject GetObject(MonsterTypeId monsterTypeId, Transform parent)
        {
            GameObject result = null;

            if (_enemyObjectsStock.Count > 0)
            {
                result = _enemyObjectsStock[0];
                _enemyObjectsStock.RemoveAt(0);
            }
            else
            { 
               result =  _gameFactory.CreateMonster(monsterTypeId, parent);
            }
            result.SetActive(true);
            return result;
        }

        public void ReturnObject(GameObject obj)
        {
            
            _enemyObjectsStock.Add(obj);
        }
    }
}
