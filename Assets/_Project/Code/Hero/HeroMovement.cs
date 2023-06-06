using System;
using Code.Data;
using Code.Infrastructure.InputSystem;
using Code.Logic;
using Code.Services;
using Code.Services.Input;
using Code.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Code.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMovement : MonoBehaviour, ISave
    {
        private CharacterController _characterController;
        private IInputSystem _input;

        private readonly float _movementSpeed = 10f;
        private readonly float _rollForce = 20f;

        [Inject]
        private void Construct(IInputSystem input)
        {
            _input = input;
        }
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
        
        private void OnDisable()
        {
            _input.Controls.Disable();
        }

        private void Start()
        {
            _input.Controls.Enable();
        }

        private void Awake()
        {
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

            var moveAxis = _input.Controls.Player.Movement.ReadValue<Vector2>();
            

            if (moveAxis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = Camera.main.transform.TransformDirection(moveAxis);
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