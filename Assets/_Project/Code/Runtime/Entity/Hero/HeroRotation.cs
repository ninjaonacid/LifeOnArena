using Code.Runtime.Logic;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    public class HeroRotation : MonoBehaviour
    {
        private PlayerControls _controls;
        public float rotationSmooth;
        public float rotationSpeed;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        [Inject]
        private void Constructor(PlayerControls controls)
        {
            _controls = controls;
        }

        private void Update()
        {
            LookRotation();
        }

        private void LookRotation()
        {
            if (_controls.Player.Movement.ReadValue<Vector2>().sqrMagnitude > Constants.Epsilon)
            {
                var rotateFrom = transform.rotation;
                var directionVector = _camera.transform.TransformDirection(_controls.Player.Movement.ReadValue<Vector2>());
                directionVector.y = 0;
                directionVector.Normalize();
                var rotateTo = Quaternion.LookRotation(directionVector);
                transform.rotation = Quaternion.Slerp(rotateFrom, rotateTo,  rotationSmooth * rotationSpeed * Time.deltaTime );
            }
        }
    }
}
