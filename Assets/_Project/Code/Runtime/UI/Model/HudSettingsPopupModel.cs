using Code.Runtime.Data;

namespace Code.Runtime.UI.Model
{
    public class HudSettingsPopupModel : IScreenModel, ISavableModel
    {
        public bool IsSoundOn { get; private set;}
        public bool IsMusicOn { get; private set;}
        
        private readonly AudioData _audioData;
        
        public HudSettingsPopupModel(AudioData audioData)
        {
            _audioData = audioData;
        }

        public void ChangeMusicState()
        {
            IsMusicOn = !IsMusicOn;
            _audioData.isMusicOn = IsMusicOn;
        }

        public void ChangeSoundState()
        {
            IsSoundOn = !IsSoundOn;
            _audioData.isSoundOn = IsSoundOn;
        }
        
        public void Initialize()
        {
            IsMusicOn = _audioData.isMusicOn;
            IsSoundOn = _audioData.isSoundOn;
        }

        public void LoadData()
        {
            IsMusicOn = _audioData.isMusicOn;
            IsSoundOn = _audioData.isSoundOn;
        }

        public void SaveModelData()
        {
            _audioData.isMusicOn = IsMusicOn;
            _audioData.isSoundOn = IsSoundOn; 
        }
    }
}