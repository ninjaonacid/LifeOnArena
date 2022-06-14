using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public SaveLoadService(IPersistentProgressService progressService,
            IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }


        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
        }


        public void SaveProgress()
        {
            foreach (var progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }
    }
}