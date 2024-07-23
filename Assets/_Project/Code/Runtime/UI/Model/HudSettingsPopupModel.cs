using Code.Runtime.Services.PersistentProgress;

namespace Code.Runtime.UI.Model
{
    public class HudSettingsPopupModel : IScreenModel
    {
        public bool IsSoundOn;
        public bool IsMusicOn;
        
        private readonly IGameDataContainer _gameData;
        
        public HudSettingsPopupModel(IGameDataContainer gameData)
        {
            _gameData = gameData;
        }
        
        public void Initialize()
        {
            IsSoundOn = _gameData.AudioData.isSoundMuted;
            IsMusicOn = _gameData.AudioData.isMusicMuted;
        }
    }
}