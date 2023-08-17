using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Infrastructure.InputSystem
{
    public interface IInputSystem
    {
        void Enable();
        void Disable();
        PlayerControls.PlayerActions Player { get; }
        PlayerControls.UIActions UI { get; }
        
    }
}
