using Code.Runtime.Services.PersistentProgress;

namespace Code.Runtime.UI.Model
{
    public class MainMenuSettingsPopupModel : IScreenModel, ISavableModel
    {
        public bool IsSoundOn { get; private set;}
        public bool IsMusicOn { get; private set;}
        
        private readonly IGameDataContainer _gameData;
        
        public MainMenuSettingsPopupModel(IGameDataContainer gameData)
        {
            _gameData = gameData;
        }
        
        public void Initialize()
        {
            IsMusicOn = _gameData.AudioData.isMusicOn;
            IsSoundOn = _gameData.AudioData.isSoundOn;
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
            IsMusicOn = _gameData.AudioData.isMusicOn;
            IsSoundOn = _gameData.AudioData.isSoundOn;
        }

        public void SaveModelData()
        {
            _gameData.AudioData.isMusicOn = IsMusicOn;
            _gameData.AudioData.isSoundOn = IsSoundOn;
        }
    }
}