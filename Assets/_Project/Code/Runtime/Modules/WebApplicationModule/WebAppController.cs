using Code.Runtime.Core.Audio;
using Code.Runtime.Services.PauseService;
using Code.Runtime.Services.SaveLoad;
using InstantGamesBridge.Modules.Game;
using UnityEngine.Device;
using VContainer.Unity;

namespace Code.Runtime.Modules.WebApplicationModule
{
    public class WebAppController : IInitializable
    {
        private readonly AudioService _audioService;
        private readonly PauseService _pauseService;
        private SaveLoadService _saveLoad;

        public WebAppController(AudioService audioService, PauseService pauseService, SaveLoadService saveLoad)
        {
            _audioService = audioService;
            _pauseService = pauseService;
            _saveLoad = saveLoad;
        }

        public void Initialize()
        {
            if (WebApplication.IsWebApp)
            {
                Application.targetFrameRate = 60;
                InstantGamesBridge.Bridge.game.visibilityStateChanged += HandleVisibilityLogic;
            }
        }

        private void HandleVisibilityLogic(VisibilityState state)
        {
            switch (state)
            {
                case VisibilityState.Hidden:
                    _pauseService.PauseGame();
                    _audioService.PauseAll();
                    _saveLoad.SaveData();
                    break;
                case VisibilityState.Visible:
                    _pauseService.UnpauseGame();
                    _audioService.UnpauseAll();
                    break;
            }
        }
    }
}
