using Code.Logic;
using UnityEngine;
using VContainer;

namespace Code.Entity.Hero
{
    public class HeroRotation : MonoBehaviour
    {
        private PlayerControls _controls;
        public float rotationSmooth;
        public float rotationSpeed;

        [Inject]
        private void Constructor(PlayerControls controls)
        {
            _controls = controls;
        }

        private void Update()
        {
            LookRotation();
        }

        public void LookRotation()
        {
            
            if (_controls.Player.Movement.ReadValue<Vector2>().sqrMagnitude > Constants.Epsilon)
            {
                var rotateFrom = transform.rotation;
                var directionVector = Camera.main.transform.TransformDirection(_controls.Player.Movement.ReadValue<Vector2>());
                directionVector.y = 0;
                directionVector.Normalize();
                var rotateTo = Quaternion.LookRotation(directionVector);
                transform.rotation = Quaternion.Slerp(rotateFrom, rotateTo,  rotationSmooth * rotationSpeed * Time.deltaTime );
            }
        }
    }
}
