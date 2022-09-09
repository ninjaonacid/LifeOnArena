using System.Collections.Generic;
using Code.Data;
using Code.Services.PersistentProgress;
using Code.UI;
using UnityEngine;

namespace Code.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void Cleanup();
        void RegisterProgressWatchers(GameObject go);
        void Register(ISavedProgressReader progressReader);
    }
}