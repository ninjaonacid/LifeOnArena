using UnityEngine;

namespace Code.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Interact = "Interact";
        private const string Attack = "Attack";
        private const string Skill1 = "Skill1";
        private const string Skill2 = "Skill2";
        private const string Skill3 = "Skill3";

        public abstract Vector2 Axis { get; }

        public bool IsButtonPressed(string buttonKey)
        {
            return SimpleInput.GetButtonUp(buttonKey);
        }

        public bool IsAttackButtonUp()
        {
            return SimpleInput.GetButtonUp(Attack);
        }

        public bool IsSkillButton1()
        {
            return SimpleInput.GetButtonUp(Skill1);
        }

        public bool IsSkillButton2()
        {
            return SimpleInput.GetButtonUp(Skill2);
        }

        public bool IsSkillButton3()
        {
            return SimpleInput.GetButtonUp(Skill3);
        }

        public bool IsInteractButtonUp()
        {
            return SimpleInput.GetButtonUp(Interact);
        }
        protected static Vector2 SimpleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal),
                SimpleInput.GetAxis(Vertical));
        }
    }
}