using System.Collections.Generic;
using Code.Data;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
        List<ISaveReader> ProgressReaders { get; }
        List<ISave> ProgressWriters { get; }
        void Cleanup();
        void RegisterProgressWatchers(GameObject go);
        void Register(ISaveReader progressReader);
        void SaveProgressAtPath();
        void LoadSaveData();
    }
}