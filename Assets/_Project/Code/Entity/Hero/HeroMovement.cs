using Code.Data;
using Code.Data.PlayerData;
using Code.Logic;
using Code.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Code.Entity.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMovement : MonoBehaviour, ISave
    {
        [SerializeField] private HeroStats _stats;
        private CharacterController _characterController;
        private PlayerControls _controls;

        private float MovementSpeed => _stats.Stats["MoveSpeed"].Value;
        
        private readonly float _rollForce = 20f;

        [Inject]
        private void Construct(PlayerControls controls)
        {
            _controls = controls;
        }
        public void UpdateData(PlayerData data)
        {
            data.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(),
                transform.position.AsVectorData());
        }

        public void LoadData(PlayerData data)
        {
            if (CurrentLevel() == data.WorldData.PositionOnLevel.Level)
            {
                var savedPosition = data.WorldData.PositionOnLevel.Position;
                if (savedPosition != null) Warp(savedPosition);
            }
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void ForceMove()
        {
            var movementVector = Vector3.zero;

            if (_controls.Player.Movement.ReadValue<Vector2>().sqrMagnitude > Constants.Epsilon )
            {
                movementVector = Camera.main.transform.TransformDirection(_controls.Player.Movement.ReadValue<Vector2>());
                movementVector.y = 0;
                movementVector.Normalize();
            }

            movementVector += Physics.gravity;

            _characterController.Move(movementVector * _rollForce * Time.deltaTime);
        }

        public void Movement(bool isForced = false)
        { 
            var movementVector = Vector3.zero;

            var moveAxis = _controls.Player.Movement.ReadValue<Vector2>();
            

            if (moveAxis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = Camera.main.transform.TransformDirection(moveAxis);
                movementVector.y = 0;
                movementVector.Normalize();
            }

            movementVector += Physics.gravity;
            
            _characterController.Move(movementVector * MovementSpeed * Time.deltaTime);
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