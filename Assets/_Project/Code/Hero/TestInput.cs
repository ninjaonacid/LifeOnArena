using Code.Services;
using Code.Services.Input;
using Code.UI;
using Code.UI.Services;
using UnityEngine;

namespace Code.Hero
{
    public class TestInput : MonoBehaviour
    {
        private IWindowService _windowService;
        private IInputService _inputService;
        private void Awake()
        {
            _windowService = ServiceLocator.Container.Single<IWindowService>();
            _inputService = ServiceLocator.Container.Single<IInputService>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _windowService.Open(UIWindowID.UpgradeMenu);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                _inputService.DisableInput();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                _inputService.EnableInput();
            }
        }
    }
}
