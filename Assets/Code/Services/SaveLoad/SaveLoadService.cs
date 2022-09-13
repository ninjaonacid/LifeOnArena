using System.Collections.Generic;
using System.IO;
using Code.Data;
using Code.Services.PersistentProgress;
using Code.UI;
using UnityEngine;

namespace Code.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        private readonly IPersistentProgressService _progressService;

        public SaveLoadService(IPersistentProgressService progressService)
        {
            _progressService = progressService;
            
        }
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();

        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public void Cleanup()
        {
            ProgressWriters.Clear();
            ProgressReaders.Clear();
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
        }

        public void SaveProgress()
        {
            foreach (var progressWriter in ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public void SaveProgressAtPath()
        {
           string json = _progressService.Progress.ToJson();
           File.WriteAllText(Application.dataPath + "/save.txt", json);
        }

        public void RegisterProgressWatchers(GameObject go)
        {
            foreach (var progressReader in
                     go.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }


        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}