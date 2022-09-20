using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Code.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Attack = "Attack";
        private const string Skill1 = "Skill1";
        private const string Skill2 = "Skill2";
        private const string Skill3 = "Skill3";

        public abstract Vector2 Axis { get; }

        public bool isAttackOrSkillPressed()
        {
            return isAttackButtonUp() || isSkillButton1() || isSkillButton2()
                || isSkillButton3();
        }

        public bool isAttackButtonUp()
        {
            return SimpleInput.GetButtonUp(Attack);
        }

        public bool isSkillButton1()
        {
            return SimpleInput.GetButtonUp(Skill1);
        }

        public bool isSkillButton2()
        {
            return SimpleInput.GetButtonUp(Skill2);
        }

        public bool isSkillButton3()
        {
            return SimpleInput.GetButtonUp(Skill3);
        }

        protected static Vector2 SimpleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal),
                SimpleInput.GetAxis(Vertical));
        }
    }
}