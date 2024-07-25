using Code.Runtime.Services.PersistentProgress;

namespace Code.Runtime.UI.Model
{
    public class HudSettingsPopupModel : IScreenModel, ISavableModel
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
            IsSoundOn = _gameData.AudioData.isSoundOn;
            IsMusicOn = _gameData.AudioData.isMusicOn;
        }

        public void LoadData()
        {
            _gameData.AudioData.isMusicOn = IsMusicOn;
            _gameData.AudioData.isSoundOn = IsSoundOn; 
        }

        public void SaveModelData()
        {
            _gameData.AudioData.isMusicOn = IsMusicOn;
            _gameData.AudioData.isSoundOn = IsSoundOn;
        }
    }
}