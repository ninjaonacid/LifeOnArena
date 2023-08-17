using System.Collections.Generic;
using System.IO;
using Code.Data;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        private readonly IGameDataService _gameDataService;

        public SaveLoadService(IGameDataService gameDataService)
        {
            _gameDataService = gameDataService;
        }
        public List<ISaveReader> ProgressReaders { get; } = new List<ISaveReader>();

        public List<ISave> ProgressWriters { get; } = new List<ISave>();

        public void Cleanup()
        {
            ProgressWriters.Clear();
            ProgressReaders.Clear();
        }

        public PlayerData LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerData>();
        }

        public void SaveProgress()
        {
            foreach (var progressWriter in ProgressWriters)
                progressWriter.UpdateData(_gameDataService.PlayerData);

            PlayerPrefs.SetString(ProgressKey, _gameDataService.PlayerData.ToJson());
            SaveProgressAtPath();
        }

        public void LoadSaveData()
        {
            foreach (ISaveReader progressReader in ProgressReaders)
                progressReader.LoadData(_gameDataService.PlayerData);
        }

        public void SaveProgressAtPath()
        {
           string json = _gameDataService.PlayerData.ToJson();
           File.WriteAllText(Application.dataPath + "/save.txt", json);
        }

        public void RegisterProgressWatchers(GameObject go)
        {
            foreach (var progressReader in
                     go.GetComponentsInChildren<ISaveReader>())
                Register(progressReader);
        }


        public void Register(ISaveReader progressReader)
        {
            if (progressReader is ISave progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}