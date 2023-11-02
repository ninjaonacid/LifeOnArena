using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Buttons
{
    public class MusicButton : BaseButton
    {
        [SerializeField] private Image OnImage;
        [SerializeField] private Image OffImage;

        public void SetButton(bool isMuted)
        {
            if (isMuted)
            {
                OffImage.enabled = false;
                OnImage.enabled = true;
            }
            else
            {
                OnImage.enabled = true;
                OffImage.enabled = false;
            }
        }
    }
}
