using System.Collections.Generic;
using System.IO;
using Code.Runtime.Data;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine;

namespace Code.Runtime.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PlayerDataKey = "PlayerData";
        private const string AudioDataKey = "AudioData";
        private readonly IGameDataContainer _gameDataContainer;

        public SaveLoadService(IGameDataContainer gameDataContainer)
        {
            _gameDataContainer = gameDataContainer;
        }

        public List<ISaveLoader> ProgressReaders { get; } = new List<ISaveLoader>();

        public List<ISave> ProgressWriters { get; } = new List<ISave>();

        public void Cleanup()
        {
            ProgressWriters.Clear();
            ProgressReaders.Clear();
        }

        public AudioData LoadAudioData()
        {
            return PlayerPrefs.GetString(AudioDataKey)
                .ToDeserialized<AudioData>();
        }

        public PlayerData LoadPlayerData()
        {
            return PlayerPrefs.GetString(PlayerDataKey)?
                .ToDeserialized<PlayerData>();
        }

        public void SaveData()
        {
            foreach (var progressWriter in ProgressWriters)
                progressWriter.UpdateData(_gameDataContainer.PlayerData);

            PlayerPrefs.SetString(PlayerDataKey, _gameDataContainer.PlayerData.ToJson());
            PlayerPrefs.SetString(AudioDataKey, _gameDataContainer.AudioData.ToJson());
            
            SaveProgressAtPath();
        }

        public void LoadData()
        {
            foreach (ISaveLoader progressReader in ProgressReaders)
                progressReader.LoadData(_gameDataContainer.PlayerData);
        }

        public void SaveProgressAtPath()
        {
           string json = _gameDataContainer.PlayerData.ToJson();
           File.WriteAllText(Application.dataPath + "/save.txt", json);
        }

        public void RegisterProgressWatchers(GameObject go)
        {
            foreach (var progressReader in
                     go.GetComponentsInChildren<ISaveLoader>())
                Register(progressReader);
        }


        public void Register(ISaveLoader progressLoader)
        {
            if (progressLoader is ISave progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressLoader);
        }
    }
}