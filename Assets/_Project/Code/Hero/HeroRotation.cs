using Code.Infrastructure.InputSystem;
using Code.Logic;
using Code.Services.Input;
using UnityEngine;
using VContainer;

namespace Code.Hero
{
    public class HeroRotation : MonoBehaviour
    {
        private IInputSystem _input;
        public float rotationSmooth;
        public float rotationSpeed;

        [Inject]
        private void Constructor(IInputSystem input)
        {
            _input = input;
        }

        private void Update()
        {
            LookRotation();
        }

        public void LookRotation()
        {
            
            if (_input.Controls.Player.Movement.ReadValue<Vector2>().sqrMagnitude > Constants.Epsilon)
            {
                var rotateFrom = transform.rotation;
                var directionVector = Camera.main.transform.TransformDirection(_input.Controls.Player.Movement.ReadValue<Vector2>());
                directionVector.y = 0;
                directionVector.Normalize();
                var rotateTo = Quaternion.LookRotation(directionVector);
                transform.rotation = Quaternion.Slerp(rotateFrom, rotateTo,  rotationSmooth * rotationSpeed * Time.deltaTime );
            }
        }
    }
}
