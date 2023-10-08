using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Buttons
{
    public class MusicButton : MonoBehaviour
    {
        [SerializeField] private Image OnImage;
        [SerializeField] private Image OffImage;
        
        public Button Button;
        
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
