using UnityEngine;

namespace Code.Infrastructure.InputSystem
{
    public interface IInputSystem
    {
        Vector2 Axis { get; }
        
        PlayerControls Controls { get; }
        
    }
}
