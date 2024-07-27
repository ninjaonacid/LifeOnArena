using Code.Runtime.Core.Audio;
using Code.Runtime.Services.PauseService;
using InstantGamesBridge.Modules.Game;
using UnityEngine.Device;
using VContainer.Unity;

namespace Code.Runtime.Modules.WebApplicationModule
{
    public class WebAppController : IInitializable
    {
        private AudioService _audioService;
        private PauseService _pauseService;

        public WebAppController(AudioService audioService, PauseService pauseService)
        {
            _audioService = audioService;
            _pauseService = pauseService;
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
                    _audioService.UnpauseAll();
                    break;
                case VisibilityState.Visible:
                    _pauseService.UnpauseGame();
                    _audioService.UnpauseAll();
                    break;
            }
        }
    }
}
