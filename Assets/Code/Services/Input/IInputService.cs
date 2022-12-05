using UnityEngine;

namespace Code.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool isAttackOrSkillPressed();
        bool isAttackButtonUp();
        bool isSkillButton1();
        bool isSkillButton2();
        bool isSkillButton3();

        bool isInteractButtonUp();
    }
}