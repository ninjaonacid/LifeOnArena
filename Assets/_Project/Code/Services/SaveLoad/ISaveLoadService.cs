using System.Collections.Generic;
using Code.Data;
using Code.Data.PlayerData;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveData();
        PlayerData LoadPlayerData();
        List<ISaveReader> ProgressReaders { get; }
        List<ISave> ProgressWriters { get; }
        void Cleanup();
        void RegisterProgressWatchers(GameObject go);
        void Register(ISaveReader progressReader);
        void SaveProgressAtPath();
        void LoadData();
        AudioData LoadAudioData();
    }
}