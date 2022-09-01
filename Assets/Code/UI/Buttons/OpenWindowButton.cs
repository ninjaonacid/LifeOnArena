using Code.UI.Services;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Code.UI.Buttons
{
    public class OpenWindowButton : MonoBehaviour
    {
        public Button Button;
        public UIWindowID WindowId;
        private IWindowService _windowService;

        public void Construct(IWindowService windowService)
        {
            _windowService = windowService;
        }
        private void Awake() =>
            Button.onClick.AddListener(Open);



        private void Open() =>
            _windowService.Open(WindowId);


    }
}
