using Code.UI.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Code.UI.Buttons
{
    public class OpenWindowButton : MonoBehaviour
    {
        public Button Button;
        public ScreenID WindowId;
        private IScreenService _screenService;
        
        public void Construct(IScreenService screenService)
        {
            _screenService = screenService;
        }

        private void Open()
        {
            _screenService.Open(WindowId);
            Debug.Log("Clicked" + gameObject.name);
        }
    }
}
