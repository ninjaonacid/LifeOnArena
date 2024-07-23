using Code.Runtime.Services.PersistentProgress;

namespace Code.Runtime.UI.Model
{
    public class MainMenuSettingsPopupModel : IScreenModel, ISavableModel
    {
        public bool IsSoundOn;
        public bool IsMusicOn;
        
        private readonly IGameDataContainer _gameData;
        
        public MainMenuSettingsPopupModel(IGameDataContainer gameData)
        {
            _gameData = gameData;
        }
        
        public void Initialize()
        {
            IsMusicOn = _gameData.AudioData.isMusicMuted;
            IsSoundOn = _gameData.AudioData.isSoundMuted;
        }

        public void ChangeSoundState(bool value)
        {
            IsSoundOn = value;
        }

        public void ChangeMusicState(bool value)
        {
            IsMusicOn = value;
        }

        public void LoadData()
        {
            _gameData.AudioData.isMusicMuted = IsMusicOn;
            _gameData.AudioData.isSoundMuted = IsSoundOn;
        }

        public void SaveModelData()
        {
            _gameData.AudioData.isMusicMuted = IsMusicOn;
            _gameData.AudioData.isSoundMuted = IsSoundOn;
        }
    }
}