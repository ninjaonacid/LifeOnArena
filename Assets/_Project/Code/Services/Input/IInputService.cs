using UnityEngine;

namespace Code.Services.Input
{
    public interface IInputService : IService
    {
        void DisableInput();
        void EnableInput();
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
        bool IsSkillButton1();
        bool IsSkillButton2();
        bool IsSkillButton3();

        bool IsInteractButtonUp();
        bool IsButtonPressed(string buttonKey);
    }
}