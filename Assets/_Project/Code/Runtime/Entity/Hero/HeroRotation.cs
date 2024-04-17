using Code.Runtime.Logic;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    public class HeroRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSmooth;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Transform _playerModel;
        
        private PlayerControls _controls;
        private Camera _camera;
        private bool _isEnabled = true;

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
            if (_isEnabled is true)
            {
                LookRotation();
            }
            
        }

        public void EnableRotation(bool value)
        {
            _isEnabled = value;
        }

        public void Rotate(float rotationSpeed)
        {
            Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotationSpeed * Time.deltaTime);
            transform.rotation *= deltaRotation;
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
                transform.rotation = Quaternion.Slerp(rotateFrom, rotateTo,  _rotationSmooth * _rotationSpeed * Time.deltaTime );
            }
        }
    }
}
