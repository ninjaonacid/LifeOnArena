using Code.Data;
using Code.Logic;
using Code.Services;
using Code.Services.Input;
using Code.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMovement : MonoBehaviour, ISave
    {
        private CharacterController _characterController;
        private IInputService _input;

        private readonly float _movementSpeed = 10f;
        private readonly float _rollForce = 20f;

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(),
                transform.position.AsVectorData());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                var savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null) Warp(savedPosition);
            }
        }

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
            _characterController = GetComponent<CharacterController>();
        }

        public void ForceMove()
        {
            bool isVectorFind;
            var movementVector = Vector3.zero;

            if (_input.Axis.sqrMagnitude > Constants.Epsilon )
            {
                movementVector = Camera.main.transform.TransformDirection(_input.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
            }

            movementVector += Physics.gravity;

            _characterController.Move(movementVector * _rollForce * Time.deltaTime);
        }

        public void Movement(bool isForced = false)
        { 
            var movementVector = Vector3.zero;

            if (_input.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = Camera.main.transform.TransformDirection(_input.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
            }

            movementVector += Physics.gravity;

            _characterController.Move(movementVector * _movementSpeed * Time.deltaTime);
        }

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private static string CurrentLevel()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}