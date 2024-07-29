using Code.Runtime.Core.Audio;
using Code.Runtime.Services.PauseService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.SaveLoad;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Game;
using UnityEngine.Device;
using VContainer.Unity;

namespace Code.Runtime.Modules.WebApplicationModule
{
    public class WebAppController : IInitializable
    {
        private readonly AudioService _audioService;
        private readonly PauseService _pauseService;
        private readonly SaveLoadService _saveLoad;
        private readonly IGameDataContainer _gameData;
        
        public WebAppController(AudioService audioService, PauseService pauseService, SaveLoadService saveLoad, IGameDataContainer gameData)
        {
            _audioService = audioService;
            _pauseService = pauseService;
            _saveLoad = saveLoad;
            _gameData = gameData;
        }

        public void Initialize()
        {
            if (WebApplication.IsWebApp)
            {
                Application.targetFrameRate = 60;
                Bridge.game.visibilityStateChanged += HandleVisibilityLogic;
            }
        }

        private void HandleVisibilityLogic(VisibilityState state)
        {
            switch (state)
            {
                case VisibilityState.Hidden:
                    _pauseService.PauseGame();
                    _audioService.MuteAll(true);
                    _saveLoad.SaveData();
                    break;
                case VisibilityState.Visible:
                    _pauseService.UnpauseGame();
                    _audioService.MuteMusic(!_gameData.AudioData.isMusicOn);
                    _audioService.MuteSounds(!_gameData.AudioData.isSoundOn);
                    break;
            }
        }
    }
}
