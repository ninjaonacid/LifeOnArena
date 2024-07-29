using Code.Runtime.Data;
using Code.Runtime.Services.PersistentProgress;

namespace Code.Runtime.UI.Model
{
    public class MainMenuSettingsPopupModel : IScreenModel, ISavableModel
    {
        public bool IsSoundOn { get; private set;}
        public bool IsMusicOn { get; private set;}

        private readonly AudioData _audioData;
        
        public MainMenuSettingsPopupModel(AudioData audioData)
        {
            _audioData = audioData;
        }

        public void ChangeMusicState(bool value)
        {
            IsMusicOn = value;
            _audioData.isMusicOn = IsMusicOn;
        }

        public void ChangeSoundState(bool value)
        {
            IsSoundOn = value;
            _audioData.isSoundOn = IsSoundOn;
        }
        public void Initialize()
        {
            _audioData.isMusicOn = IsMusicOn;
            _audioData.isSoundOn = IsSoundOn; 
        }

        public void LoadData()
        {
            _audioData.isMusicOn = IsMusicOn;
            _audioData.isSoundOn = IsSoundOn; 
        }

        public void SaveModelData()
        {
            _audioData.isMusicOn = IsMusicOn;
            _audioData.isSoundOn = IsSoundOn; 
        }
    }
}