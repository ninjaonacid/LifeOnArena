using Code.Runtime.Services.PersistentProgress;

namespace Code.Runtime.UI.Model
{
    public class MainMenuSettingsPopupModel : IScreenModel, ISavableModel
    {
        public bool SoundButton;
        public bool MusicButton;
        
        private readonly IGameDataContainer _gameData;
        
        public MainMenuSettingsPopupModel(IGameDataContainer gameData)
        {
            _gameData = gameData;
        }
        
        public void Initialize()
        {
            MusicButton = _gameData.AudioData.isMusicMuted;
            SoundButton = _gameData.AudioData.isSoundMuted;
        }

        public void ChangeSoundState(bool value)
        {
            SoundButton = value;
        }

        public void ChangeMusicState(bool value)
        {
            MusicButton = value;
        }

        public void LoadData()
        {
            _gameData.AudioData.isMusicMuted = MusicButton;
            _gameData.AudioData.isSoundMuted = SoundButton;
        }

        public void SaveModelData()
        {
            _gameData.AudioData.isMusicMuted = MusicButton;
            _gameData.AudioData.isSoundMuted = SoundButton;
        }
    }
}