using Code.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Buttons
{
    public class OpenWindowButton : MonoBehaviour
    {
        public Button Button;
        public ScreenID WindowId;
        private IScreenViewService _screenViewService;

        public void Construct(IScreenViewService screenViewService)
        {
            _screenViewService = screenViewService;
        }
        private void Awake() =>
            Button.onClick.AddListener(Open);

        private void Open()
        {
            _screenViewService.Open(WindowId);
            Debug.Log("Clicked" + gameObject.name);
        }
    }
}
