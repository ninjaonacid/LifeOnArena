using Code.Runtime.Services.PersistentProgress;

namespace Code.Runtime.UI.Model
{
    public class HudSettingsPopupModel : IScreenModel
    {
        public bool IsSoundOn { get; private set;}
        public bool IsMusicOn { get; private set;}
        
        private readonly IGameDataContainer _gameData;
        
        public HudSettingsPopupModel(IGameDataContainer gameData)
        {
            _gameData = gameData;
        }

        public void ChangeMusicState(bool value)
        {
            IsMusicOn = value;
        }

        public void ChangeSoundState(bool value)
        {
            IsSoundOn = value;
        }
        public void Initialize()
        {
            IsSoundOn = _gameData.AudioData.isSoundMuted;
            IsMusicOn = _gameData.AudioData.isMusicMuted;
        }
    }
}