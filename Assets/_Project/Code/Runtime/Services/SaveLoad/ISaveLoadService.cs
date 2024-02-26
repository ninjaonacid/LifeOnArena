using System.Collections.Generic;
using Code.Runtime.Data;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine;

namespace Code.Runtime.Services.SaveLoad
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