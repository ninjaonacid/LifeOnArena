using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Buttons
{
    
    public class SoundButton : BaseButton
    {
        [SerializeField] private Image OnImage;
        [SerializeField] private Image OffImage;

        public void SetButton(bool isSoundOn)
        {
            if (isSoundOn)
            {
                OnImage.enabled = true;
                OffImage.enabled = false;
                _buttonImage = OnImage;
            }
            else
            {
                OnImage.enabled = false;
                OffImage.enabled = true;
                _buttonImage = OffImage;
            }
        }
    }
}
