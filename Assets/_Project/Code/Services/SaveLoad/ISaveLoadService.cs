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
        List<ISaveLoader> ProgressReaders { get; }
        List<ISave> ProgressWriters { get; }
        void Cleanup();
        void RegisterProgressWatchers(GameObject go);
        void Register(ISaveLoader progressLoader);
        void SaveProgressAtPath();
        void LoadData();
        AudioData LoadAudioData();
    }
}