using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Buttons
{
    public class MusicButton : BaseButton
    {
        [SerializeField] private Image OnImage;
        [SerializeField] private Image OffImage;

        public void SetButton(bool isMusicOn)
        {
            if (isMusicOn)
            {
                OffImage.enabled = false;
                OnImage.enabled = true;
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
