using System.Collections.Generic;
using System.IO;
using Code.Data;
using Code.Data.PlayerData;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.Services.SaveLoad
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

        public List<ISaveReader> ProgressReaders { get; } = new List<ISaveReader>();

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
            foreach (ISaveReader progressReader in ProgressReaders)
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