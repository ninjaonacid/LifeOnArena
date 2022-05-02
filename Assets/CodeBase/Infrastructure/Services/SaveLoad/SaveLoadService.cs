using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService, 
                             IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        
        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?
            .ToDeserialized<PlayerProgress>();
        
        

        public void SaveProgress()
        {
            foreach(ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_progressService.Progress);
            }
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }
    }
}
