using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Buttons
{
    public class MusicButton : BaseButton
    {
        [SerializeField] private CanvasElement _onImage;
        [SerializeField] private CanvasElement _offImage;

        public void SetButton(bool isSoundOn)
        {
            if (isSoundOn)
            {
                _onImage.Show();
                _offImage.Hide();
            }
            else
            {
                _onImage.Hide();
                _offImage.Show();
            }
        }
    }
}
