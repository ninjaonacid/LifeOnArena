using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;


namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Register(ISavedProgressReader progressReader);
        void Cleanup();
        LootPiece CreateLoot();
        GameObject CreateHero(GameObject initialPoint);
        GameObject CreateHud();
        GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent);
        void CreateSpawner(Vector3 at, string spawnerDataId, MonsterTypeId spawnerDataMonsterTypeId);
        
    }
}