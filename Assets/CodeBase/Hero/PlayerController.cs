using UnityEngine;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour, ISavedProgress
    {

        private CharacterController _characterController;
        private float _movementSpeed = 10f;

        private IInputService _input;


        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Movement();
        }

         private void Movement()
        {
            Vector3 movementVector = Vector3.zero;
           
                if (_input.Axis.sqrMagnitude > Constants.Epsilon)
                {
                   movementVector = Camera.main.transform.TransformDirection(_input.Axis);
                   movementVector.y = 0;
                   movementVector.Normalize();

                   transform.forward = movementVector;
                }
            
            movementVector += Physics.gravity;

            _characterController.Move(movementVector * _movementSpeed * Time.deltaTime);
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
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null)
                {
                    Warp(to: savedPosition);
                }
            }
        }

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private static string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
        
    }
}